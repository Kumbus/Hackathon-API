using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MSSMRepository : ISlotsRepository
    {
        private readonly TaskOrganiserContext _context;
        
        public MSSMRepository(TaskOrganiserContext context)
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

        public Task<ActivitySlot> GetAllSlotsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ActivitySlot> GetSlotByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSlotAsync(ActivitySlot slot)
        {
            throw new NotImplementedException();
        }
    }
}
