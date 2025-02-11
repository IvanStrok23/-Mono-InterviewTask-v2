using Microsoft.EntityFrameworkCore;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Interfaces;
using Moq;

public class DummyContext : IDataContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<UserVehicle> UserVehicles { get; set; }
    public DbSet<VehicleModelEntity> VehicleModels { get; set; }
    public DbSet<VehicleBrandEntity> VehicleBrands { get; set; }

    private readonly Dictionary<Type, object> _dbSets;
    private int _nextUserId = 1;
    public DummyContext()
    {
        Users = GetMockDbSet(new List<UserEntity>());
        UserTokens = GetMockDbSet(new List<UserToken>());
        UserVehicles = GetMockDbSet(new List<UserVehicle>(), e => e.UserId);

        VehicleModels = GetMockDbSet(new List<VehicleModelEntity>());
        VehicleBrands = GetMockDbSet(new List<VehicleBrandEntity>());

        _dbSets = new Dictionary<Type, object>
        {
            { typeof(UserEntity), Users },
            { typeof(UserToken), UserTokens },
            { typeof(UserVehicle), UserVehicles },
            { typeof(VehicleModelEntity), VehicleModels },
            { typeof(VehicleBrandEntity), VehicleBrands }
        };
    }

    private DbSet<T> GetMockDbSet<T>(List<T> data) where T : class, IEntity
    {
        var queryableData = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities => data.AddRange(entities));
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => data.Remove(entity));
        mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities =>
        {
            foreach (var entity in entities) data.Remove(entity);
        });

        mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
               .Returns<object[]>(ids => data.FirstOrDefault(e => e.GetId() == (int)ids[0]));

        return mockSet.Object;
    }

    private DbSet<T> GetMockDbSet<T>(List<T> data, Func<T, object> keySelector = null) where T : class
    {
        var queryableData = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        // Setup IQueryable behavior for LINQ queries
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

        // Setup Add, AddRange, Remove, RemoveRange methods
        mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
        mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities => data.AddRange(entities));
        mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => data.Remove(entity));
        mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities =>
        {
            foreach (var entity in entities) data.Remove(entity);
        });



        // Optional: Setup Find method if a keySelector is provided
        if (keySelector != null)
        {
            mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
                   .Returns<object[]>(ids => data.FirstOrDefault(e => keySelector(e).Equals(ids[0])));
        }

        return mockSet.Object;
    }
    private DbSet<T> GetDbSet<T>() where T : class, IEntity
    {
        if (_dbSets.TryGetValue(typeof(T), out var dbSet))
        {
            return dbSet as DbSet<T>;
        }
        throw new InvalidOperationException($"DbSet for type {typeof(T).Name} not found.");
    }

    public Task SaveChangesAsync() => Task.CompletedTask;

    public Task<T> Get<T>(int id) where T : class, IEntity
    {
        var dbSet = GetDbSet<T>();
        return Task.FromResult(dbSet.Find(id));
    }

    public Task<int> Insert<T>(T entity) where T : class, IEntity
    {
        // For UserEntity, assign a new ID
        if (entity is UserEntity user)
        {
            user.Id = _nextUserId++;  // Increment the ID for the next user
        }

        var dbSet = GetDbSet<T>();
        dbSet.Add(entity);
        return Task.FromResult(entity.GetId());
    }

    public Task Insert<T>(List<T> entities) where T : class, IEntity
    {
        foreach (var entity in entities)
        {
            // Assign IDs for UserEntities
            if (entity is UserEntity user)
            {
                user.Id = _nextUserId++;
            }
        }

        var dbSet = GetDbSet<T>();
        dbSet.AddRange(entities);
        return Task.CompletedTask;
    }

    public Task<bool> Remove<T>(T entity) where T : class, IEntity
    {
        var dbSet = GetDbSet<T>();
        dbSet.Remove(entity);
        return Task.FromResult(true);
    }

    public Task Remove<T>(List<T> entities) where T : class, IEntity
    {
        var dbSet = GetDbSet<T>();
        dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }
}