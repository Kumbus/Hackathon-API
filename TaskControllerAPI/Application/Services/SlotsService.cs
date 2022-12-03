using Application.AdditionalStructures;
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
        private readonly IHexIdRepository _hexIdRepository;
        private readonly IMapper _mapper;
        private readonly IPlannedTasksRepository _plannedTasksRepository;
          

        public SlotsService(ISlotsRepository slotsRepository, IMapper mapper, IHexIdRepository hexIdRepository, IPlannedTasksRepository plannedTasksRepository)
        {
            _slotsRepository = slotsRepository;
            _mapper = mapper;
            _hexIdRepository = hexIdRepository;
            _plannedTasksRepository= plannedTasksRepository;
        }

        public async Task<SlotDto> AddSlotAsync(PostSlotDto newSlot)
        {
            var user = await _hexIdRepository.GetUser(newSlot.HexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();
            var slot = _mapper.Map<ActivitySlot>(newSlot);
            slot.UserId = user.UserId;
            await _slotsRepository.AddSlotAsync(slot);

            return _mapper.Map<SlotDto>(slot);
        }

        public async Task DeleteSlotAsync(Guid id, string hexIdentificator)
        {
            var user = await _hexIdRepository.GetUser(hexIdentificator);
            if (user is null)
                throw new InvalidCredentialsException();

            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new SlotNotFoundException(id);

            if (slot.UserId == user.UserId)
                await _slotsRepository.DeleteSlotAsync(slot);
            else
                throw new InvalidCredentialsException();
        }

        public async Task<SlotDto> GetSlotByIdAsync(Guid id)
        {
            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new SlotNotFoundException(id);

            return _mapper.Map<SlotDto>(slot);
        }

        public async Task<IEnumerable<SlotDto>> GetSlotsAsync(string hexIdentificator)
        {
            var identification = await _hexIdRepository.GetUser(hexIdentificator);
            if (identification is null)
                throw new InvalidCredentialsException();
            var slots = await _slotsRepository.GetAllSlotsAsync(identification.UserId);
            if (slots == null)
                throw new UserNotFoundException(identification.UserId);

            return _mapper.Map<IEnumerable<SlotDto>>(slots);
        }

        public async Task<SlotDto> UpdateSlotAsync(UpdateSlotDto updatedSlot, Guid id)
        {
            var identification = await _hexIdRepository.GetUser(updatedSlot.HexIdentificator);
            if (identification is null)
                throw new InvalidCredentialsException();

            var slot = await _slotsRepository.GetSlotByIdAsync(id);
            if (slot == null)
                throw new SlotNotFoundException(id);

            if (slot.UserId != identification.UserId)
                throw new InvalidCredentialsException();

            var newSlot = _mapper.Map(updatedSlot, slot);

            await _slotsRepository.UpdateSlotAsync(newSlot);

            return _mapper.Map<SlotDto>(newSlot);
        }

        public async Task<GetWeekDto> GetWeek(DateTime dateTime, string hexIdentificator)
        {
            var identification = await _hexIdRepository.GetUser(hexIdentificator);
            if (identification is null)
                throw new InvalidCredentialsException();

            var week = await _slotsRepository.GetWeek(dateTime, identification.UserId);
            GetWeekDto getWeekDto= new GetWeekDto();
            Weekday weekday = new Weekday();
            weekday.slotWithTasks = new List<SlotWithTasks>();
            List<Weekday> weekdays = new List<Weekday>();
            getWeekDto.Week = new List<Weekday>();
            foreach ( var item in week)
            {
                List<SlotWithTasks> slotWithTasksList = new List<SlotWithTasks>();
                foreach(var item2 in item)
                {
                    var tasks = await _plannedTasksRepository.GetTasksIdsBySlotId(item2.Id);
                    SlotWithTasks slotWithTasks = new SlotWithTasks();
                    slotWithTasks.Start = item2.Start;
                    slotWithTasks.QuartersNumber = item2.QuartersNumber;
                    slotWithTasks.Id = item2.Id;
                    slotWithTasks.UserId = item2.UserId;
                    slotWithTasks.TasksIds = tasks;
                    slotWithTasksList.Add(new SlotWithTasks(slotWithTasks));
                }
                weekdays.Add(new Weekday(slotWithTasksList));
                
                //getWeekDto.Week.Add(weekday);
            }
            getWeekDto.Week = new List<Weekday>(weekdays);
            return getWeekDto;
        }
    }
}
