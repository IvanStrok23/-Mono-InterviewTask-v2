using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.UI.WebApi.Auth.UserPrincipal;

public interface IUserPrincipal
{
    UserEntity User { get; }

    bool IsAdmin { get; }
    bool IsClient { get; }
    bool IsSuperAdmin { get; }

    void SetUser(UserEntity user);
}
