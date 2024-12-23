using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Core.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Auth.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            // Получаем пользователя
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => new User
                {
                    Id = u.Id,
                    Login = u.Login,
                    Score = u.Score,
                    PasswordHash = u.PasswordHash,
                    Salt = u.Salt,
                    Tasks = _context.Tasks
                        .Where(t => t.UserId == u.Id) // Фильтруем задачи по userId
                        .Select(t => new Core.Models.Task
                        {
                            Id = t.Id,
                            UserId = u.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Priority = t.Priority,
                            IsCompleted = t.IsCompleted,
                            Subtasks = _context.Subtasks
                                .Where(s => s.TaskId == t.Id) // Фильтруем подзадачи по taskId
                                .Select(s => new Subtask
                                {
                                    Id = s.Id,
                                    TaskId = t.Id,
                                    Title = s.Title,
                                    IsCompleted = s.IsCompleted
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            // Получаем пользователя
            var user = await _context.Users
                .Where(u => u.Login == login)
                .Select(u => new User
                {
                    Id = u.Id,
                    Login = u.Login,
                    Score = u.Score,
                    PasswordHash = u.PasswordHash,
                    Salt = u.Salt,
                    Tasks = _context.Tasks
                        .Where(t => t.UserId == u.Id) // Фильтруем задачи по userId
                        .Select(t => new Core.Models.Task
                        {
                            Id = t.Id,
                            UserId = u.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Priority = t.Priority,
                            IsCompleted = t.IsCompleted,
                            Subtasks = _context.Subtasks
                                .Where(s => s.TaskId == t.Id) // Фильтруем подзадачи по taskId
                                .Select(s => new Subtask
                                {
                                    Id = s.Id,
                                    TaskId = t.Id,
                                    Title = s.Title,
                                    IsCompleted = s.IsCompleted
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            if (user != null)
            {
                user.Login = userDto.Login;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
