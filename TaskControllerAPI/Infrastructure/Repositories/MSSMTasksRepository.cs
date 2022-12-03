using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MSSMTasksRepository : IPlannedTasksRepository
    {
        private readonly TaskOrganiserContext _context;

        public MSSMTasksRepository(TaskOrganiserContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(PlannedTask newTask)
        {
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(PlannedTask task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<PlannedTask> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<PlannedTask>> GetTasksBySlotIdAsync(Guid slotId)
        {
            var tasks = _context.Tasks
                .Where(t => t.SlotId == slotId);

            return await tasks.ToListAsync();
        }

        public async Task<IEnumerable<PlannedTask>> GetTasksByUserIdAsync(string userId)
        {
            var tasks = _context.Tasks
                .Where(t => t.UserId == userId);

            return await tasks.ToListAsync();
        }

        public async Task UpdateTaskAsync(PlannedTask updatedTask)
        {
            _context.Update(updatedTask);
            await _context.SaveChangesAsync();
        }
    }
}
