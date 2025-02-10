using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Infrastructure.Data.Entities;

public class VehicleBrandEntity : BaseEntity, IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public virtual ICollection<VehicleModelEntity> Models { get; set; }

    public int GetId()
    {
        return Id;
    }

}
