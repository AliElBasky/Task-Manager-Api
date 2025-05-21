using Microsoft.AspNetCore.Mvc;
using Task_Manager_API.Models;
using Task_Manager_API.Services;

namespace Task_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetTask")]
        public async Task<ActionResult<TaskItem>> GetTask([FromBody] int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        [Route("AddTask")]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] TaskItem task)
        {
            var createdTask = await _service.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskItem task)
        {
            var updated = await _service.UpdateTaskAsync(task.Id, task);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteTask")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _service.DeleteTaskAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
