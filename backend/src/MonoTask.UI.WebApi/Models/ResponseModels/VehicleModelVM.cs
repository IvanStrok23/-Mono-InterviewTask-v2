namespace MonoTask.UI.WebApi.Models.ResponseModels;

public class VehicleModelVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? BrandId { get; set; }
    public string BrandName { get; set; }
    public int Year { get; set; }
}
