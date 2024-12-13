using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PlanItBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var userWithTasks = await _userService.GetUserByIdAsync(userId);
            if (userWithTasks == null)
            {
                return NotFound("User not found");
            }

            var response = new
            {
                User = userWithTasks
            };

            return Ok(response);
        }
    }
}
