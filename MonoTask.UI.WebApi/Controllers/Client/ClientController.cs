using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Client;
using MonoTask.UI.WebApi.Auth.UserPrincipal;

namespace MonoTask.UI.WebApi.Controllers.Client;

[Authorize(Policy = "IsClient")]
[Route("api/client")]
public class ClientController : ControllerBase
{
    private readonly IUserPrincipal _userPrincipal;
    private readonly IClientVehicleServices _vehicleServices;

    public ClientController(IUserPrincipal userPrincipal, IClientVehicleServices vehicleServices)
    {
        _userPrincipal = userPrincipal;
        _vehicleServices = vehicleServices;
    }

    [HttpGet("get-vehicles")]
    public async Task<IActionResult> GetVehicleModels()
    {
        var results = await _vehicleServices.GetClientVehicles(_userPrincipal.User.Id);
        return Ok(results);
    }

    [HttpPost("add-vehicle")]
    public async Task<IActionResult> AddVehicleModels(int vehicleId)
    {
        await _vehicleServices.AddClientVehicle(_userPrincipal.User.Id, vehicleId);
        return Ok();
    }
}
