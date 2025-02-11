using MonoTask.Core.Entities.Entities;

namespace MonoTask.Core.Interfaces.Services;

public interface IUserServices
{
    Task<User> InsertUser(string name);
    Task<User> RefreshToken(string refreshToken);
}
