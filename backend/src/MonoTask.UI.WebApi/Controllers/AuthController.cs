using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.UI.WebApi.Models.RequestModels;
using MonoTask.UI.WebApi.Models.ResponseModels;

namespace MonoTask.UI.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserServices _userService;
    private readonly IMapper _mapper;

    public AuthController(IUserServices userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToken = await _userService.InsertUser(registrationDto.Name, registrationDto.Email, registrationDto.Password);
        return Ok(_mapper.Map<UserTokenResponse>(userToken));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userToken = await _userService.GetUserToken(loginDto.Email, loginDto.Password);
            return Ok(_mapper.Map<UserTokenResponse>(userToken));
        }
        catch (ArgumentException)
        {
            return Unauthorized("Invalid credentials.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
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
            return Ok(_mapper.Map<UserToken>(user));
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
