using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Tests.Helpers.Builders;

public class TokenBuilder
{
    private UserToken _token;

    public TokenBuilder(int userId)
    {
        _token = new UserToken(userId)
        {
            Id = 1,
            AccessToken = "",
            RefreshToken = "",
            AccessTokenExpiry = DateTime.UtcNow,
            User = new()
            {
                Id = userId,
                TokenId = 1
            },
        };
    }

    public TokenBuilder WithId(int id)
    {
        _token.Id = id;
        return this;
    }

    public TokenBuilder WithUser(UserEntity user)
    {
        _token.User = user;
        _token.UserId = user.Id;
        return this;
    }

    public TokenBuilder WithAccessToken(string token)
    {
        _token.AccessToken = token;
        return this;
    }

    public TokenBuilder WithRefreshToken(string token)
    {
        _token.RefreshToken = token;
        return this;
    }

    public TokenBuilder WithAccessTokenExpiry(DateTime tokenExpiry)
    {
        _token.AccessTokenExpiry = tokenExpiry;
        return this;
    }

    public UserToken Build()
    {
        return _token;
    }
}
