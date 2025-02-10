using MonoTask.Core.Entities.Entities;
using MonoTask.Core.Models.Dtos;

namespace MonoTask.Core.Interfaces.Services;

public interface IVehicleBrandService
{
    Task<int> InsertMake(VehicleBrand entity);
    Task<VehicleBrand> GetBrandId(int id);
    Task<bool> UpdateBrand(VehicleBrand model);
    Task<List<VehicleBrand>> GetBrands(PagingParams filterData);
    Task<bool> DeleteBrand(int id);
}
