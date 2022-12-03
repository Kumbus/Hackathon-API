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
        private readonly IHexIdRepository _hexIdRepository;
        private readonly ISlotsRepository _slotsRepository;
        public TasksService(IMapper mapper, IValidator<PlannedTask> validator, IPlannedTasksRepository repository, IHexIdRepository hexIdRepository, ISlotsRepository slotsRepository)
        {
            _mapper = mapper;
            _validator = validator;
            _repository = repository;
            _hexIdRepository = hexIdRepository;
            _slotsRepository = slotsRepository;
        }

        public async Task<TaskDto> AddTaskAsync(PostTaskDto newTask)
        {
            var user = await _hexIdRepository.GetUser(newTask.HexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var task = _mapper.Map<PlannedTask>(newTask);
            task.UserId = user.UserId;

            ValidationResult result = await _validator.ValidateAsync(task);
            if (!result.IsValid)
                throw new FailedOnCreation("Priority should be from range 1 to 5. Estimated time should be from 15 minutes to 1440 minutes.");

            await _repository.AddTaskAsync(task);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task AssignTask(Guid taskId, string hexIdentificator, Guid slotId)
        {
            var identificator = await _hexIdRepository.GetUser(hexIdentificator);
            if(identificator is null) 
                throw new InvalidCredentialsException();

            var task = await _repository.GetTaskByIdAsync(taskId);
            if(task is null) 
                throw new InvalidCredentialsException();

            var slot = await _slotsRepository.GetSlotByIdAsync(slotId);
            if(slot is null)
                throw new InvalidCredentialsException();

            task.SlotId = slotId;
            await _repository.UpdateTaskAsync(task);

            await Task.CompletedTask;
        }

        public async Task ChangeStateOfTaskAsync(Guid id, string hexIdentificator, bool isCompleted)
        {
            var user = await _hexIdRepository.GetUser(hexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            if (user.UserId != task.UserId)
                throw new InvalidCredentialsException();

            task.IsCompleted = isCompleted;

            await _repository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(Guid id, string hexIdentificator)
        {
            var user = await _hexIdRepository.GetUser(hexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            if(user.UserId != task.UserId) 
                throw new InvalidCredentialsException();

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

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string hexIdentificator)
        {
            var user = await _hexIdRepository.GetUser(hexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var tasks = await _repository.GetTasksByUserIdAsync(user.UserId);

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> UpdateTaskAsync(UpdateTaskDto updatedTask, Guid id)
        {
            var user = await _hexIdRepository.GetUser(updatedTask.HexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var task = await _repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            if (user.UserId != task.UserId)
                throw new InvalidCredentialsException();

            var newTask = _mapper.Map(updatedTask, task);

            ValidationResult result = await _validator.ValidateAsync(newTask);
            if (!result.IsValid)
                throw new FailedOnCreation("Priority should be from range 1 to 5. Estimated time should be from 15 minutes to 1440 minutes");

            await _repository.UpdateTaskAsync(newTask);

            return _mapper.Map<TaskDto>(newTask);
        }

    }
}
