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
        public Task<ActivitySlot> GetAllSlotsAsync();
        public Task UpdateSlotAsync(ActivitySlot slot);
    }
}
