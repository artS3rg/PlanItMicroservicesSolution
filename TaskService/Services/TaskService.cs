using Core.DTOs;
using Core.Interfaces;
using Core.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Data.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Subtasks)
                .ToListAsync();
            return tasks.Select(MapToTaskDto);
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Subtasks)
                .SingleOrDefaultAsync(t => t.Id == taskId);
            return task != null ? MapToTaskDto(task) : null;
        }

        public async Task AddTaskAsync(CreateTaskDto taskDto)
        {
            var task = MapToTask(taskDto);
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(UpdateTaskDto taskDto)
        {
            var task = await _context.Tasks
                .Include(t => t.Subtasks)
                .FirstOrDefaultAsync(t => t.Id == taskDto.Id);
            if (task != null)
            {
                task.Title = taskDto.Title;
                task.Description = taskDto.Description;
                task.Priority = taskDto.Priority;
                task.IsCompleted = taskDto.IsCompleted;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        private TaskDto MapToTaskDto(Core.Models.Task task)
        {
            return new TaskDto
            {
                Id = task.Id,
                UserId = task.UserId,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                IsCompleted = task.IsCompleted,
                Subtasks = task.Subtasks.Select(s => new SubtaskDto
                {
                    Id = s.Id,
                    TaskId = s.TaskId,
                    Title = s.Title,
                    IsCompleted = s.IsCompleted
                }).ToList()
            };
        }

        private Core.Models.Task MapToTask(CreateTaskDto taskDto)
        {
            return new Core.Models.Task
            {
                UserId = taskDto.UserId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                IsCompleted = false,
            };
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Subtasks)
                .FirstOrDefaultAsync(t => t.Id == taskId);
            if (task != null)
            {
                task.Title = task.Title;
                task.Description = task.Description;
                task.Priority = task.Priority;
                task.IsCompleted = true;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
