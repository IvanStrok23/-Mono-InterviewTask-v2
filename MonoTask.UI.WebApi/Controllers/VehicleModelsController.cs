using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Core.Models.Dtos;
using MonoTask.UI.WebApi.Extensions;
using MonoTask.UI.WebApi.RequestModels;

namespace MonoTask.UI.WebApi.Controllers;

[Route("api/vehicle-models")]
public class VehicleModelsController : ControllerBase
{
    private readonly IVehicleModelService _vehicleModelService;
    public VehicleModelsController(IVehicleModelService vehicleModelService)
    {
        _vehicleModelService = vehicleModelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetModels([FromQuery] TableFilterData sortingData)
    {
        PagingParams param = new PagingParams(sortingData.Page, sortingData.SortBy.ToSortByEnum(), sortingData.SortOrder.ToSortOrderEnum(), sortingData.SearchValue);
        var results = await _vehicleModelService.GetModels(param);
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> AddStaticData()
    {
        await _vehicleModelService.AddTestData();
        return Ok();
    }
}
