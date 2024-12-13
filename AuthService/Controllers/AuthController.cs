using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Auth.Services;

namespace PlanItBack.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequestDto request)
        {
            var response = await _authService.RegisterUserAsync(request.Login, request.Password);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.AuthenticateUserAsync(request.Login, request.Password);
            return Ok(response);
        }
    }

}
