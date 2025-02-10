using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonoTask.Core.Entities.Entities;
using MonoTask.Core.Interfaces.Client;
using MonoTask.Infrastructure.Data.Interfaces;

namespace MonoTask.Application.Services.Client;

public class ClientVehicleServices : IClientVehicleServices
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;

    public ClientVehicleServices(IDataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VehicleModel>> GetClientVehicles(int clientId)
    {
        return await _context.UserVehicles
            .Where(uv => uv.UserId == clientId)
            .Include(uv => uv.Vehicle)
                .ThenInclude(v => v.VehicleBrand)
            .Select(uv => _mapper.Map<VehicleModel>(uv.Vehicle))
            .ToListAsync();
    }

    public async Task AddClientVehicle(int clientId, int vehicleId)
    {
        await _context.UserVehicles.AddAsync(new() { UserId = clientId, VehicleId = vehicleId });
        await _context.SaveChangesAsync();
    }
}
