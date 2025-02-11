using System.ComponentModel.DataAnnotations;

namespace MonoTask.UI.WebApi.Models.RequestModels;

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "RefreshToken is required.")]
    public string RefreshToken { get; set; }
}
