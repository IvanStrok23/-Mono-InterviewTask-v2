using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.UI.WebApi.Models.RequestModels;

namespace MonoTask.UI.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserServices _userService;

    public AuthController(IUserServices userService)
    {
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
