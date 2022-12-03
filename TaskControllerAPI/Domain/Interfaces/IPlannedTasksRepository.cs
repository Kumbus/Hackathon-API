using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPlannedTasksRepository
    {
        public Task AddTaskAsync(PlannedTask newTask);
        public Task<PlannedTask> GetTaskByIdAsync(Guid id);
        public Task<IEnumerable<PlannedTask>> GetTasksByUserIdAsync(string userId);
        public Task<IEnumerable<PlannedTask>> GetTasksBySlotIdAsync(Guid slotId);
        public Task DeleteTaskAsync(PlannedTask task);
        public Task UpdateTaskAsync(PlannedTask updatedTask);
    }
}
