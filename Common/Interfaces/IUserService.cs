using Core.DTOs;
using Core.Models;
using Task = System.Threading.Tasks.Task;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByLoginAsync(string login);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(UserDto userDto);
    }
}
