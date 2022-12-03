using Application.Dtos.SlotsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISlotsService
    {
        public Task<SlotDto> AddSlotAsync(PostSlotDto newSlot);
        public Task<SlotDto> UpdateSlotAsync(UpdateSlotDto updatedSlot, Guid id);
        public Task DeleteSlotAsync(Guid id);
        public Task<SlotDto> GetSlotByIdAsync(Guid id);
        public Task<IEnumerable<SlotDto>> GetSlotsAsync(string userId);
    }
}
