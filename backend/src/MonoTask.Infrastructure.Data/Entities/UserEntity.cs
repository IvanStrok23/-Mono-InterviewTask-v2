using MonoTask.Infrastructure.Data.Helpers;
using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Infrastructure.Data.Entities;

public class UserEntity : BaseEntity, IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoles Roles { get; set; }

    public int? TokenId { get; set; }
    public virtual UserToken Token { get; set; }

    //todo: is this needed?
    public ICollection<UserVehicle> UserVehicles { get; set; } = new List<UserVehicle>();

    public int GetId() => Id;

    public bool IsAdmin => Roles.HasFlag(UserRoles.IsAdmin);
    public bool IsClient => Roles.HasFlag(UserRoles.IsClient);
    public bool IsSuperAdmin => Roles.HasFlag(UserRoles.IsSuperAdmin);
}
