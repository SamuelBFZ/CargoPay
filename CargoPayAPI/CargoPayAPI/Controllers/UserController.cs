using CargoPayAPI.DAL.Entities;
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
        public async Task<ActionResult<User>> CreateUserAsync(User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return Ok(createdUser);
        }
    }
}
