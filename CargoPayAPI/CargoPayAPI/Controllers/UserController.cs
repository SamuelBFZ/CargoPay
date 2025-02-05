using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Models;
using CargoPayAPI.Models.DTOs.User;
using CargoPayAPI.Services.Interfaces;
using CargoPayAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoPayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = createUserDto.UserName,
                Email = createUserDto.Email,
                Role = (UserRole)createUserDto.Role,
                PasswordHash = await _userService.HashPassword(createUserDto.PasswordHash),
            };

            var createdUser = await _userService.CreateUserAsync(user);

            var userDto = new UserDto
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Role = (int)createdUser.Role,
            };

            return Ok(userDto);
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            if (id == null) return BadRequest("Id necessary");

            var user = await _userService.GetUserByIdAsync(id);

            if (user == null) return NotFound($"ID {id} not found");

            return Ok(user);
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);

            if (deletedUser == null) return NotFound("User ID not found");

            return Ok($"{deletedUser.Username} deleted");
        }

        [HttpPost, ActionName("Authenticate")]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto authenticateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResult = await _userService.AuthenticateAsync(authenticateUserDto.Username, authenticateUserDto.Password);

            if (!authResult.Success)
            {
                return Unauthorized(authResult.Error);
            }

            return Ok(new { token = authResult.Token, refreshToken = authResult.RefreshToken });
        }
    }
}
