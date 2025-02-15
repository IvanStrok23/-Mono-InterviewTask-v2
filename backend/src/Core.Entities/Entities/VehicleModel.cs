﻿namespace MonoTask.Core.Entities.Entities;

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int BrandId { get; set; }
    public VehicleBrand Brand { get; set; }
    public int Year { get; set; }

    //todo: hide those in base
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
