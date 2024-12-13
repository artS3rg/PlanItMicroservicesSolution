using Core.Interfaces;
using Core.Models;
using Core.DTOs;
using Auth.Utils;

namespace Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _repository;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthService(IUserService repository, JwtTokenGenerator tokenGenerator)
        {
            _repository = repository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterUserAsync(string login, string password)
        {
            var (hash, salt) = PasswordHasher.HashPassword(password);
            var user = new User { Login = login, PasswordHash = hash, Salt = salt };
            await _repository.AddUserAsync(user);
            var token = _tokenGenerator.GenerateToken(user);
            return new AuthResponseDto { Token = token };
        }

        public async Task<AuthResponseDto> AuthenticateUserAsync(string login, string password)
        {
            var user = await _repository.GetUserByLoginAsync(login);
            if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))
                throw new UnauthorizedAccessException("Invalid login or password");
            var token = _tokenGenerator.GenerateToken(user);
            return new AuthResponseDto { Token = token };
        }
    }

}
