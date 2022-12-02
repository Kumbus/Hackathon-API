using Application.Dtos.SlotsDtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    [Route("slots")]
    [ApiController]
    public class SlotsController : ControllerBase
    {
        private readonly ISlotsService _slotsService;

        public SlotsController (ISlotsService slotsService)
        {
            _slotsService = slotsService;
        }
        [HttpPost]
        public async Task <IActionResult> AddSlot(PostSlotDto newSlot)
        {
            await _slotsService.AddSlotAsync(newSlot);
            return Ok(newSlot);
        }
    }
    
}
