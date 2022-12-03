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
    public class MSSMSlotsRepository : ISlotsRepository
    {
        private readonly TaskOrganiserContext _context;
        
        public MSSMSlotsRepository(TaskOrganiserContext context)
        {
            _context = context;
        }
        public async Task AddSlotAsync(ActivitySlot slot)
        {
            await _context.AddAsync(slot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlotAsync(ActivitySlot slot)
        {
            _context.Remove(slot);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActivitySlot>> GetAllSlotsAsync(string userId)
        {
            var slots = _context.ActivitySlots
                .Where(s => s.UserId == userId);

            return await slots.ToListAsync();
                
        }

        public async Task<ActivitySlot> GetSlotByIdAsync(Guid id)
        {
            return await _context.ActivitySlots.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSlotAsync(ActivitySlot slot)
        {
            _context.Update(slot);
            await _context.SaveChangesAsync();
        }
    }
}
