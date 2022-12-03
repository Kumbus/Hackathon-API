using Application.Dtos.SlotsDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("slots")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly ISlotsService _slotsService;

        public SlotsController(ISlotsService slotsService)
        {
            _slotsService = slotsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSlot(PostSlotDto newSlot)
        {
            var slot = await _slotsService.AddSlotAsync(newSlot);
            return Ok(slot);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetSlot(Guid id)
        {
           var slot = await _slotsService.GetSlotByIdAsync(id);
            return Ok(slot);
        }

        [HttpPost("/slots/allSlots/{hexIdentificator}")]
        public async Task<IActionResult> GetSlots(string hexIdentificator)
        {
            var slots = await _slotsService.GetSlotsAsync(hexIdentificator);
            return Ok(slots);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSlot(UpdateSlotDto updatedSlot, Guid id)
        {
            var slot = await _slotsService.UpdateSlotAsync(updatedSlot, id);
            return Ok(slot);
        }
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(Guid id, string hexIdentificator)
        {
            await _slotsService.DeleteSlotAsync(id, hexIdentificator);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeek(DateTime dateTime, string hexIdentificator)
        {
            var week = await _slotsService.GetWeek(dateTime, hexIdentificator);
            return Ok(week); 
        }
    }
    
}
