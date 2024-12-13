using Core.DTOs;

namespace Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(string login, string password);
        Task<AuthResponseDto> AuthenticateUserAsync(string login, string password);
    }
}
