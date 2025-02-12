using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.UI.WebApi.Auth.UserPrincipal;
using MonoTask.UI.WebApi.Models.ResponseModels;

namespace MonoTask.UI.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IUserPrincipal _userPrincipal;
        private readonly IMapper _mapper;

        public UserController(IUserServices userService, IUserPrincipal _serPrincipal, IMapper mapper)
        {
            _userService = userService;
            _userPrincipal = _serPrincipal;
            _mapper = mapper;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            var user = await _userService.GetUser(_userPrincipal.User.Id);
            return Ok(_mapper.Map<UserSummary>(user));
        }
    }
}
