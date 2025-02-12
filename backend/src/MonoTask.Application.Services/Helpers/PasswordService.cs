using Microsoft.AspNetCore.Identity;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Application.Services.Helpers;

public class PasswordService
{
    private readonly PasswordHasher<UserEntity> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<UserEntity>();
    }

    public string HashPassword(UserEntity user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(UserEntity user, string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}