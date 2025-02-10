using MonoTask.Core.Entities.Entities;
using MonoTask.Core.Models.Dtos;

namespace MonoTask.Core.Interfaces.Services;

public interface IVehicleModelService
{
    Task AddTestData();
    Task<int> InsertModel(VehicleModel entity);
    Task<VehicleModel> GetModelById(int id);
    Task<bool> UpdateModel(VehicleModel model);
    Task<List<VehicleModel>> GetModels(PagingParams pagingParams);
    Task<bool> DeleteModel(int id);

}
