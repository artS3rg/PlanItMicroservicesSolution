using System.Threading.Tasks;
using Core.Interfaces;
using Core.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly AppDbContext _context;

        public StatisticService(AppDbContext context)
        {
            _context = context;
        }

        // Возвращает количество завершенных задач для пользователя
        public async Task<int> GetCompletedTaskCount(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && t.IsCompleted)
                .CountAsync();
        }

        // Возвращает количество задач, которые находятся в ожидании выполнения
        public async Task<int> GetPendingTaskCount(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId && !t.IsCompleted)
                .CountAsync();
        }

        // Возвращает словарь, где ключ - уровень приоритета, значение - количество задач с этим приоритетом
        public async Task<Dictionary<int, int>> GetTaskCountByPriority(int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .GroupBy(t => t.Priority)
                .Select(g => new { Priority = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Priority, x => x.Count);
        }
    }
}
