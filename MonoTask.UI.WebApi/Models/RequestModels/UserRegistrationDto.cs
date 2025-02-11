using System.ComponentModel.DataAnnotations;

namespace MonoTask.UI.WebApi.Models.RequestModels;

public class UserRegistrationDto
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}
