using MonoTask.Core.Entities.Entities;

namespace MonoTask.Core.Interfaces.Client;

public interface IClientVehicleServices
{
    Task<List<VehicleModel>> GetClientVehicles(int clientId);
    Task AddClientVehicle(int clientId, int vehicleId);
}
