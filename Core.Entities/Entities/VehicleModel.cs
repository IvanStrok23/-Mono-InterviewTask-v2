namespace MonoTask.Core.Entities.Entities;

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MakeId { get; set; }
    public VehicleBrand VehicleBrand { get; set; }
    public int Year { get; set; }

    //todo: hide those in base
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
