using System.ComponentModel.DataAnnotations;

namespace MonoTask.UI.WebApi.Models.RequestModels
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
