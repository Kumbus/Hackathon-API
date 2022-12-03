using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISlotsRepository
    {
        public Task AddSlotAsync(ActivitySlot slot);
        public Task DeleteSlotAsync(ActivitySlot slot);
        public Task<ActivitySlot> GetSlotByIdAsync(Guid id);
        public Task<IEnumerable<ActivitySlot>> GetAllSlotsAsync(string userId);
        public Task UpdateSlotAsync(ActivitySlot slot);
    }
}
