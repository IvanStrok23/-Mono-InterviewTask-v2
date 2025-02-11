namespace MonoTask.Infrastructure.Data.Helpers;

[Flags]
public enum UserRoles
{
    None = 0,
    IsAdmin = 1,
    IsClient = 2,
    IsSuperAdmin = 4
}
