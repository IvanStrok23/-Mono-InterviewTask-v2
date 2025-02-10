using MonoTask.Infrastructure.Data.Helpers;
using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Infrastructure.Data.Entities;

public class UserEntity : BaseEntity, IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserRoles Roles { get; set; }
    public string Token { get; set; }

    public UserEntity()
    {
        Token = Guid.NewGuid().ToString();
    }

    public int GetId() => Id;

    public bool IsAdmin => Roles.HasFlag(UserRoles.IsAdmin);
    public bool IsClient => Roles.HasFlag(UserRoles.IsClient);
    public bool IsSuperAdmin => Roles.HasFlag(UserRoles.IsSuperAdmin);
}
