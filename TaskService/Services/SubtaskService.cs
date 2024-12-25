using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Core.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Data.Services
{
    public class SubtaskService : ISubtaskService
    {
        private readonly AppDbContext _context;

        public SubtaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SubtaskDto?> GetSubtaskByIdAsync(int subtaskId)
        {
            var subtask = await _context.Subtasks
                .FirstOrDefaultAsync(t => t.Id == subtaskId);
            return subtask != null ? MapToSubtask(subtask) : null;
        }

        public async Task<IEnumerable<SubtaskDto>> GetAllSubtasksAsync(int taskId)
        {
            // Получаем все подзадачи для указанной задачи
            var subtasks = await _context.Subtasks
                .Where(s => s.TaskId == taskId)
                .ToListAsync();

            // Преобразуем подзадачи в DTO
            return subtasks.Select(s => new SubtaskDto
            {
                Id = s.Id,
                TaskId = s.TaskId,
                Title = s.Title,
                IsCompleted = s.IsCompleted
            });
        }

        public async Task AddSubtaskAsync(SubtaskDto subtaskDto)
        {
            // Проверяем, существует ли задача с указанным ID
            var taskExists = await _context.Tasks.AnyAsync(t => t.Id == subtaskDto.TaskId);

            if (!taskExists)
            {
                // Если задачи не существует, выбрасываем исключение или выводим сообщение
                throw new InvalidOperationException($"Невозможно создать подзадачу. Задача с ID {subtaskDto.TaskId} не найдена.");
            }

            // Создаем новую подзадачу на основе DTO
            var subtask = new Subtask
            {
                Title = subtaskDto.Title,
                IsCompleted = subtaskDto.IsCompleted,
                TaskId = subtaskDto.TaskId // связываем подзадачу с задачей
            };

            await _context.Subtasks.AddAsync(subtask);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubtaskAsync(SubtaskDto subtaskDto)
        {
            // Находим существующую подзадачу
            var subtask = await _context.Subtasks.FindAsync(subtaskDto.Id);
            if (subtask != null)
            {
                subtask.Title = subtaskDto.Title;
                subtask.IsCompleted = subtaskDto.IsCompleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSubtaskAsync(int subtaskId)
        {
            // Находим подзадачу и удаляем её
            var subtask = await _context.Subtasks.FindAsync(subtaskId);
            if (subtask != null)
            {
                _context.Subtasks.Remove(subtask);
                await _context.SaveChangesAsync();
            }
        }

        private SubtaskDto MapToSubtask(Subtask subtaskDto)
        {
            return new SubtaskDto
            {
                Id = subtaskDto.Id,
                TaskId = subtaskDto.TaskId,
                Title = subtaskDto.Title,
                IsCompleted = false,
            };
        }

        public async Task CompleteSubtaskAsync(int subtaskId)
        {
            // Находим существующую подзадачу
            var subtask = await _context.Subtasks.FindAsync(subtaskId);
            if (subtask != null)
            {
                subtask.Title = subtask.Title;
                subtask.IsCompleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
