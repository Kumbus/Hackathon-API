using Application.Dtos.TasksDtos;
using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IPlannedTasksService _service;

        public TasksController(IPlannedTasksService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(PostTaskDto newTask)
        {
            var task = await _service.AddTaskAsync(newTask);
            return Ok(task);
        }

        [HttpGet("tasks/user/{id}")]
        public async Task<IActionResult> GetTasksByUserId(string userId)
        {
            var tasks = await _service.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("tasks/slot/{id}")]
        public async Task<IActionResult> GetTasksBySlotId(Guid slotId)
        {
            var tasks = await _service.GetTasksBySlotIdAsync(slotId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _service.DeleteTaskAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(UpdateTaskDto newTask, Guid id)
        {
            var task = await _service.UpdateTaskAsync(newTask, id);
            return Ok(task);
        }
    }
}
