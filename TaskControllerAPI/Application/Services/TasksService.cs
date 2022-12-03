using Application.Dtos.TasksDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TasksService : IPlannedTasksService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<PlannedTask> _validator;
        private readonly IPlannedTasksRepository _repository;

        public TasksService(IMapper mapper, IValidator<PlannedTask> validator, IPlannedTasksRepository repository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
        }

        public async Task<TaskDto> AddTaskAsync(PostTaskDto newTask)
        {
            var task = _mapper.Map<PlannedTask>(newTask);

            ValidationResult result = await _validator.ValidateAsync(task);
            if (!result.IsValid)
                throw new FailedOnCreation("Priority should be from range 1 to 5. Estimated time should be from 15 minutes to 1440 minutes.");

            await _repository.AddTaskAsync(task);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            await _repository.DeleteTaskAsync(task);
        }

        public async Task<TaskDto> GetTaskByIdAsync(Guid id)
        {
            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksBySlotIdAsync(Guid slotId)
        {
            var tasks = await _repository.GetTasksBySlotIdAsync(slotId);

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string userId)
        {
            var tasks = await _repository.GetTasksByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> UpdateTaskAsync(UpdateTaskDto updatedTask, Guid id)
        {
            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            var newTask = _mapper.Map(updatedTask, task);

            ValidationResult result = await _validator.ValidateAsync(newTask);
            if (!result.IsValid)
                throw new FailedOnCreation("Priority should be from range 1 to 5. Estimated time should be from 15 minutes to 1440 minutes");

            await _repository.UpdateTaskAsync(newTask);

            return _mapper.Map<TaskDto>(newTask);
        }
    }
}
