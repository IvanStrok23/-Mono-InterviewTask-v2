namespace MonoTask.UI.WebApi.Models.ResponseModels;

public class UserTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiry { get; set; }
}
