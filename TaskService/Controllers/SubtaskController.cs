using Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace PlanItBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtaskController : ControllerBase
    {
        private readonly ISubtaskService _subtaskService;
        private readonly MessageBroker _broker;

        public SubtaskController(ISubtaskService subtaskService, MessageBroker broker)
        {
            _subtaskService = subtaskService;
            _broker = broker;
        }

        [HttpGet("{subtaskId}")]
        public async Task<IActionResult> GetSubtaskById(int subtaskId)
        {
            var task = await _subtaskService.GetSubtaskByIdAsync(subtaskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("all/{taskId}")]
        public async Task<IActionResult> GetAllSubtaskByTaskId(int taskId)
        {
            var subtasks = await _subtaskService.GetAllSubtasksAsync(taskId);
            if (subtasks == null)
            {
                return NotFound();
            }
            return Ok(subtasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubtask([FromBody] SubtaskDto subtaskDto)
        {
            _broker.Publish("subtask.created", subtaskDto.Title);
            await _subtaskService.AddSubtaskAsync(subtaskDto);
            return Ok(subtaskDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubtask([FromBody] SubtaskDto subtaskDto)
        {
            _broker.Publish("subtask.updated", subtaskDto.Title);
            await _subtaskService.UpdateSubtaskAsync(subtaskDto);
            return NoContent();
        }

        [HttpDelete("{subtaskId}")]
        public async Task<IActionResult> DeleteSubtask(int subtaskId)
        {
            var subtask = await _subtaskService.GetSubtaskByIdAsync(subtaskId);
            _broker.Publish("subtask.deleted", subtask.Title);
            await _subtaskService.DeleteSubtaskAsync(subtaskId);
            return NoContent();
        }

        [HttpGet("complete/{subtaskId}")]
        public async Task<IActionResult> CompleteSubtask(int subtaskId)
        {
            var subtask = await _subtaskService.GetSubtaskByIdAsync(subtaskId);
            _broker.Publish("subtask.completed", subtask.Title);
            await _subtaskService.CompleteSubtaskAsync(subtaskId);
            return Ok();
        }
    }
}
