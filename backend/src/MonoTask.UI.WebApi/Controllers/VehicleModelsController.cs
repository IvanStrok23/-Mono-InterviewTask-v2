using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Core.Models.Dtos;
using MonoTask.UI.WebApi.Extensions;
using MonoTask.UI.WebApi.Models.RequestModels;
using MonoTask.UI.WebApi.Models.ResponseModels;

namespace MonoTask.UI.WebApi.Controllers;

[Route("api/vehicle-models")]
public class VehicleModelsController : ControllerBase
{
    private readonly IVehicleModelService _vehicleModelService;
    private readonly IMapper _mapper;

    public VehicleModelsController(IVehicleModelService vehicleModelService, IMapper mapper)
    {
        _vehicleModelService = vehicleModelService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetModels([FromQuery] TableFilterData sortingData)
    {
        PagingParams param = new PagingParams(sortingData.Page, sortingData.SortBy.ToSortByEnum(), sortingData.SortOrder.ToSortOrderEnum(), sortingData.SearchValue);
        var results = await _vehicleModelService.GetModels(param);
        return Ok(_mapper.Map<List<VehicleModelVM>>(results));
    }

    [HttpPost]
    public async Task<IActionResult> AddStaticData()
    {
        await _vehicleModelService.AddTestData();
        return Ok();
    }
}
