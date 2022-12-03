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

        [HttpPost("tasks/user/{hexIdentificator}")]
        public async Task<IActionResult> GetTasksByUserId(string hexIdentificator)
        {
            var tasks = await _service.GetTasksByUserIdAsync(hexIdentificator);
            return Ok(tasks);
        }

        [HttpPost("tasks/slot/{slotId}")]
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
        public async Task<IActionResult> DeleteTask(Guid id, string hexIdentificator)
        {
            await _service.DeleteTaskAsync(id, hexIdentificator);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(UpdateTaskDto newTask, Guid id)
        {
            var task = await _service.UpdateTaskAsync(newTask, id);
            return Ok(task);
        }

        [HttpPost("/completeTask/{id}")]
        public async Task<IActionResult> CompleteTask(Guid id, string hexIdentificator, bool isCompleted)
        {
            await _service.ChangeStateOfTaskAsync(id, hexIdentificator, isCompleted);
            return Ok();
        }
        [HttpPut("/assignTaskToSlot/{taskId}")]
        public async Task<IActionResult> AssignTask(Guid taskId, string hexIdentifier, Guid slotId)
        {
            await _service.AssignTask(taskId, hexIdentifier, slotId);
            return Ok();
        }
    }
}
