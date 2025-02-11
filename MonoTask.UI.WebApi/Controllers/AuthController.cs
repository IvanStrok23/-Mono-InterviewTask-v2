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
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.InsertUser(registrationDto.Name);
        return Ok(new { Token = user.AccessToken, UserId = user.Id });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = await _userService.RefreshToken(refreshToken.RefreshToken);
            return Ok(new { Token = user.AccessToken, UserId = user.Id });
        }
        catch (ArgumentException)
        {
            return Unauthorized("Invalid or expired refresh token.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
