using AutoMapper;
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
            Token = Guid.NewGuid().ToString()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<User>(user);
    }
}
