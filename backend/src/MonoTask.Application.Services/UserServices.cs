using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonoTask.Application.Services.Helpers;
using MonoTask.Core.Entities.Entities;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Helpers;
using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Application.Services;

public class UserServices : IUserServices
{
    private IDataContext _context;
    private readonly IMapper _mapper;
    private PasswordService _passwordService;

    public UserServices(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _passwordService = new();
    }

    public async Task<UserToken> InsertUser(string name, string email, string password)
    {
        var user = new UserEntity
        {
            Name = name,
            Email = email,
            Roles = UserRoles.IsClient,
        };
        user.Password = _passwordService.HashPassword(user, password);

        int userId = await _context.Insert(user);

        var userToken = new UserToken(userId);
        await _context.Insert(userToken);

        user.Token = userToken;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserToken>(userToken);
    }

    public async Task<UserToken> UserSummary(string name, string email, string password)
    {
        var user = new UserEntity
        {
            Name = name,
            Email = email,
            Roles = UserRoles.IsClient,
        };
        user.Password = _passwordService.HashPassword(user, password);

        int userId = await _context.Insert(user);

        var userToken = new UserToken(userId);
        await _context.Insert(userToken);

        user.Token = userToken;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserToken>(userToken);
    }

    public async Task<UserToken> GetUserToken(string email, string password)
    {
        var user = await _context.Users
            .Include(t => t.Token)
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        if (user != null && _passwordService.VerifyPassword(user, user.Password, password))
        {
            return _mapper.Map<UserToken>(user.Token);
        }
        throw new ArgumentException("Can't authorize");
    }

    public async Task<User> RefreshToken(string refreshToken)
    {
        var user = _context.Users
            .Include(x => x.Token)
            .FirstOrDefault(u => u.Token.RefreshToken == refreshToken);

        if (user == null)
        {
            throw new ArgumentException("Can't authorize");
        }
        user.Token.RefreshAccessToken();
        await _context.SaveChangesAsync();

        return _mapper.Map<User>(user);
    }

    public async Task<User> GetUser(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new ArgumentException("Can't authorize");
        }

        return _mapper.Map<User>(user);
    }
}
