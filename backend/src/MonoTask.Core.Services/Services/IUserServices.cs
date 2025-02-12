using MonoTask.Core.Entities.Entities;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Core.Interfaces.Services;

public interface IUserServices
{
    Task<User> GetUser(int id);
    Task<UserToken> GetUserToken(string email, string password);
    Task<UserToken> InsertUser(string name, string email, string password);
    Task<User> RefreshToken(string refreshToken);
}
