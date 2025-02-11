using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Infrastructure.Data.Entities;

public class UserToken : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; }

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AccessTokenExpiry { get; set; }

    public int GetId() => Id;

    public UserToken(int userId)
    {
        UserId = userId;
        AccessToken = Guid.NewGuid().ToString();
        RefreshToken = Guid.NewGuid().ToString();
        AccessTokenExpiry = DateTime.UtcNow.AddMinutes(1);
    }

    public void RefreshAccessToken()
    {
        AccessToken = Guid.NewGuid().ToString();
        AccessTokenExpiry = DateTime.UtcNow.AddMinutes(1);
    }

}
