using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    public UserServices(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User> InsertUser(string name)
    {
        var user = new UserEntity
        {
            Name = name,
            Roles = UserRoles.IsClient,
        };
        int userId = await _context.Insert(user);

        var userToken = new UserToken(userId);
        await _context.Insert(userToken);

        user.Token = userToken;
        await _context.SaveChangesAsync();

        return _mapper.Map<User>(user);
    }

    public async Task<User> RefreshToken(string refreshToken)
    {
        var user = await _context.Users
            .Include(x => x.Token)
            .FirstOrDefaultAsync(u => u.Token.RefreshToken == refreshToken);

        if (user == null)
        {
            throw new ArgumentException("Can't authorize");
        }
        user.Token.RefreshAccessToken();
        await _context.SaveChangesAsync();

        return _mapper.Map<User>(user);
    }
}
