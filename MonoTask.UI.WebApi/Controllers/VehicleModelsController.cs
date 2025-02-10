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
    public async Task<IActionResult> GetModels([FromBody] TableFilterData sortingData)
    {
        PagingParams param = new PagingParams(sortingData.Page, sortingData.SortBy.ToSortByEnum(), sortingData.SortOrder.ToSortOrderEnum(), sortingData.SearchValue);
        await _vehicleModelService.GetModels(param);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddStaticData()
    {
        await _vehicleModelService.AddTestData();
        return Ok();
    }
}
