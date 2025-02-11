using Microsoft.EntityFrameworkCore;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.Infrastructure.Data.Mappers;

namespace MonoTask.Infrastructure.Data.DbContexts;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options)
            : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<UserVehicle> UserVehicles { get; set; }
    public DbSet<VehicleModelEntity> VehicleModels { get; set; }
    public DbSet<VehicleBrandEntity> VehicleBrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityMapper());
        modelBuilder.ApplyConfiguration(new UserVehicleMapper());
        modelBuilder.ApplyConfiguration(new VehicleModelEntityMapper());
        modelBuilder.ApplyConfiguration(new VehicleBrandEntityMapper());
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
           .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public async Task<T> Get<T>(int id) where T : class, IEntity => await base.Set<T>().FindAsync(id);

    public async Task<int> Insert<T>(T entity) where T : class, IEntity
    {
        base.Set<T>().Add(entity);
        await base.SaveChangesAsync();
        return entity.GetId();
    }

    public async Task Insert<T>(List<T> entities) where T : class, IEntity
    {
        base.Set<T>().AddRange(entities);
        await base.SaveChangesAsync();
    }

    public async Task<bool> Remove<T>(T entity) where T : class, IEntity
    {
        base.Entry(entity).State = EntityState.Deleted;
        await base.SaveChangesAsync();
        return true;

    }

    public async Task Remove<T>(List<T> entities) where T : class, IEntity
    {
        base.Set<T>().RemoveRange(entities);
        await base.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

}
