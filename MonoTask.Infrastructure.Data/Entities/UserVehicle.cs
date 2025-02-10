namespace MonoTask.Infrastructure.Data.Entities;

public class UserVehicle
{
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public int VehicleId { get; set; }
    public VehicleModelEntity Vehicle { get; set; }
}
