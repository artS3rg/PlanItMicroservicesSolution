using Common;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PlanItBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly MessageBroker _broker;

        public TaskController(ITaskService taskService, MessageBroker broker)
        {
            _taskService = taskService;
            _broker = broker;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] CreateTaskDto taskDto)
        {
            _broker.Publish("task.created", taskDto.Title);
            await _taskService.AddTaskAsync(taskDto);
            return Ok(taskDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskDto taskDto)
        {
            _broker.Publish("task.updated", taskDto.Title);
            await _taskService.UpdateTaskAsync(taskDto);
            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            _broker.Publish("task.deleted", task.Title);
            await _taskService.DeleteTaskAsync(taskId);
            return NoContent();
        }

        [HttpGet("complete/{taskId}")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            _broker.Publish("task.completed", task.UserId.ToString());
            await _taskService.CompleteTaskAsync(taskId);
            return Ok("Задача выполнена.");
        }
    }
}
