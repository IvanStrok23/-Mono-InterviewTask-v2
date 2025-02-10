using Microsoft.EntityFrameworkCore;
using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.Interfaces;

public interface IDataContext
{

    DbSet<UserEntity> Users { get; set; }
    DbSet<UserVehicle> UserVehicles { get; set; }
    DbSet<VehicleModelEntity> VehicleModels { get; set; }
    DbSet<VehicleBrandEntity> VehicleBrands { get; set; }

    Task<T> Get<T>(int id) where T : class, IEntity;
    Task<int> Insert<T>(T entity) where T : class, IEntity;
    Task Insert<T>(List<T> entities) where T : class, IEntity;
    Task<bool> Remove<T>(T entity) where T : class, IEntity;
    Task Remove<T>(List<T> entities) where T : class, IEntity;
    Task SaveChangesAsync();
}
