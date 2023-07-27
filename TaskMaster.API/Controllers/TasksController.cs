using Microsoft.AspNetCore.Mvc;
using TaskMaster.API.Models;
using TaskMaster.API.Repositories.Interfaces;

namespace TaskMaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var tasks = await _taskRepository.GetTasks();
            return Ok(tasks);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetTaskById(id);
            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task)
        {
            await _taskRepository.CreateTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // PUT: api/Tasks
        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskModel task)
        {
            await _taskRepository.UpdateTask(task);
            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskById(int id)
        {
            await _taskRepository.DeleteTaskById(id);
            return NoContent();
        }

    }
}
