using Application.Dtos.SlotsDtos;
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
    }
}
