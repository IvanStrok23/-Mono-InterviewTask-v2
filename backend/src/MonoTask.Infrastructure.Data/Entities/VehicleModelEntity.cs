using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Infrastructure.Data.Entities;

public class VehicleModelEntity : BaseEntity, IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }

    public int VehicleBrandId { get; set; }
    public virtual VehicleBrandEntity VehicleBrand { get; set; }

    public ICollection<UserVehicle> UserVehicles { get; set; } = new List<UserVehicle>();

    public int GetId() => Id;
}
