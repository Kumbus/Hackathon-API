using Application.Dtos.SlotsDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SlotsService : ISlotsService
    {
        private readonly ISlotsRepository _slotsRepository;
        private readonly IMapper _mapper;

        public SlotsService(ISlotsRepository slotsRepository, IMapper mapper)
        {
            _slotsRepository = slotsRepository;
            _mapper = mapper;
        }

        public async Task<SlotDto> AddSlotAsync(PostSlotDto newSlot)
        {
            var slot = _mapper.Map<ActivitySlot>(newSlot);
            await _slotsRepository.AddSlotAsync(slot);

            return _mapper.Map<SlotDto>(slot);
        }

        public async Task DeleteSlotAsync(Guid id)
        {
            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new UserNotFoundException(id);

            await _slotsRepository.DeleteSlotAsync(slot);
        }

        public async Task<SlotDto> GetSlotByIdAsync(Guid id)
        {
            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new UserNotFoundException(id);

            return _mapper.Map<SlotDto>(slot);
        }

        public async Task<IEnumerable<SlotDto>> GetSlotsAsync(string userId)
        {
            var slots = await _slotsRepository.GetAllSlotsAsync(userId);
            if (slots == null)
                throw new UserNotFoundException(userId);

            return _mapper.Map<IEnumerable<SlotDto>>(slots);
        }

        public async Task<SlotDto> UpdateSlotAsync(UpdateSlotDto updatedSlot, Guid id)
        {
            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new UserNotFoundException(id);

            var newSlot = _mapper.Map(updatedSlot, slot);

            await _slotsRepository.UpdateSlotAsync(newSlot);

            return _mapper.Map<SlotDto>(_mapper);
        }
    }
}
