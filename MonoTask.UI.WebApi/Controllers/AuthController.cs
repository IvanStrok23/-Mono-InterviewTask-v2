using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.UI.WebApi.RequestModels;

namespace MonoTask.UI.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IDataContext _context;
        private readonly IUserServices _userService;

        public AuthController(IDataContext context, IUserServices userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] UserRegistrationDto registrationDto)
        {
            if (string.IsNullOrWhiteSpace(registrationDto.Name))
            {
                return BadRequest("Name is required.");
            }


            var user = await _userService.InsertUser(registrationDto.Name);
            return Ok(new { Token = user.Token, UserId = user.Id });
        }
    }
}
