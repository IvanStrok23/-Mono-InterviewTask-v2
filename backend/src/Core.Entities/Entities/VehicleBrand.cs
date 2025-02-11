namespace MonoTask.Core.Entities.Entities;

public class VehicleBrand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Country { get; set; } = "";

    //todo: hide those in base
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
