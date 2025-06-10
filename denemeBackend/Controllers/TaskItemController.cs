using Microsoft.AspNetCore.Mvc;
using denemeBackend.DTOs;
using denemeBackend.Services;

namespace denemeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TaskItemController(ITaskItemService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetByClientId(Guid clientId)
        {
            var tasks = await _taskService.GetByClientIdAsync(clientId);
            return Ok(tasks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetByUserId(Guid userId)
        {
            var tasks = await _taskService.GetByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskItemDto dto)
        {
            // TODO: Get actual user ID from authentication
            var userId = Guid.NewGuid(); // Temporary for testing
            var task = await _taskService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskItemDto dto)
        {
            var success = await _taskService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _taskService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
} 