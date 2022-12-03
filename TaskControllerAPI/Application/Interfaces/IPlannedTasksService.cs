using Application.Dtos.TasksDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlannedTasksService
    {
        public Task<TaskDto> AddTaskAsync(PostTaskDto newTask);
        public Task<TaskDto> UpdateTaskAsync(UpdateTaskDto updatedTask, Guid id);
        public Task DeleteTaskAsync(Guid id, string hexIdentificator);
        public Task<TaskDto> GetTaskByIdAsync(Guid id);
        public Task<IEnumerable<TaskDto>> GetTasksBySlotIdAsync(Guid slotId);
        public Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(string hexIdentificator);
        public Task ChangeStateOfTaskAsync(Guid id, string hexIdentificator, bool isCompleted);
        public Task AssignTask(Guid taskId, string hexIdentificator, Guid slotId);

    }
}
