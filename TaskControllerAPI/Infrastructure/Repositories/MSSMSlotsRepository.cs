using Application.AdditionalStructures;
using Application.Dtos.SlotsDtos;
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

        public async Task<List<List<ActivitySlot>>> GetWeek(DateTime dateTime, string userId) 
        {
            //var date = new TimeSpan(24,0,0);
            var startDate = dateTime;
            var endDate = dateTime.AddDays(1);
            //var datetmp = new TimeSpan(24,0,0);
            var monday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var tuesday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var wednesday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var thursday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var friday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var saturday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(1);
            var sunday = _context.ActivitySlots.Where(s => s.End >= startDate && s.End <= endDate && s.UserId == userId).ToList();


            List<List<ActivitySlot>> activitySlots = new List<List<ActivitySlot>>
            {
                monday,
                tuesday,
                wednesday,
                thursday,
                friday,
                saturday,
                sunday
            };

            return activitySlots;
            //GetWeekDto getWeekDto = new GetWeekDto();
            //List<Weekday> weekdays= new List<Weekday>();
            //getWeekDto.Week.Add(monday);
        }
    }
}
