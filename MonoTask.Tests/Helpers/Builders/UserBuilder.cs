using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Helpers;

namespace MonoTask.Tests.Helpers.Builders;

public class UserBuilder
{
    private UserEntity _userEntity;

    public UserBuilder()
    {
        _userEntity = new()
        {
            Id = 1,
            Name = "Test",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            TokenId = null,
            Token = null,
            UserVehicles = new List<UserVehicle>()
        };
    }

    public UserBuilder WithId(int id)
    {
        _userEntity.Id = id;
        return this;
    }

    public UserBuilder WithName(string name)
    {
        _userEntity.Name = name;
        return this;
    }

    public UserBuilder IsClient()
    {
        _userEntity.Roles |= UserRoles.IsClient;
        return this;
    }

    public UserBuilder WithToken(UserToken token)
    {
        _userEntity.Token = token;
        _userEntity.TokenId = token.Id;
        return this;
    }

    public UserEntity Build()
    {
        return _userEntity;
    }
}
