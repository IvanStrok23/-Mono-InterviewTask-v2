using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Core.Models.Dtos;
using MonoTask.Core.Models.Helpers;
using MonoTask.Infrastructure.Data.DbContexts;
using MonoTask.Infrastructure.Data.Entities;


namespace MonoTask.Application.Services.Vehicle;
using POCO = Core.Entities.Entities;

public class VehicleBrandService : IVehicleBrandService
{
    private readonly DataContext _vehiclesDbContext;
    private readonly IMapper _mapper;
    public VehicleBrandService(DataContext vehiclesDbContext, IMapper mapper)
    {
        _vehiclesDbContext = vehiclesDbContext;
        _mapper = mapper;
    }

    public async Task<int> InsertMake(POCO.VehicleBrand entity)
    {
        if (entity == null || entity.Id != 0)
        {
            return 0;
        }
        VehicleBrandEntity mapped = _mapper.Map<VehicleBrandEntity>(entity);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.UpdatedAt = DateTime.UtcNow;
        return await _vehiclesDbContext.Insert(mapped);
    }

    public async Task<POCO.VehicleBrand> GetBrandId(int id)
    {
        var result = await Task.Run(() => _vehiclesDbContext.Get<VehicleBrandEntity>(id));
        return _mapper.Map<POCO.VehicleBrand>(result);
    }


    public async Task<List<POCO.VehicleBrand>> GetBrands(PagingParams pagingParams)
    {
        var query = from make in _vehiclesDbContext.VehicleBrands
                    select make;
        sortByColumn(ref query, pagingParams.SortParams.SortBy, pagingParams.SortParams.SortOrder);
        var result = await Task.Run(() => query.Where(i => i.Name.StartsWith(pagingParams.SortParams.SearchValue)).Skip((pagingParams.Page - 1) * 10).Take(10).ToListAsync());
        return _mapper.Map<List<POCO.VehicleBrand>>(result);
    }

    public async Task<bool> UpdateBrand(POCO.VehicleBrand model)
    {
        VehicleBrandEntity temp = _vehiclesDbContext.VehicleBrands.Where(i => i.Id == model.Id).FirstOrDefault();
        if (temp == null)
        {
            return false;
        }
        else
        {
            temp.Name = model.Name;
            temp.Country = model.Country;
            temp.UpdatedAt = DateTime.UtcNow;
            await _vehiclesDbContext.SaveChangesAsync();
            return true;
        }
    }

    public async Task<bool> DeleteBrand(int id)
    {

        var m = await Task.Run(() => _vehiclesDbContext.VehicleBrands.Where(i => i.Id == id).Include(i => i.Models).FirstOrDefaultAsync());
        if (m == null)
        {
            return false;
        }
        return await _vehiclesDbContext.Remove(m);
    }

    private void sortByColumn(ref IQueryable<VehicleBrandEntity> query, SortbyEnum sortBy, SortOrderEnum sortOrder)
    {

        switch (sortBy)
        {
            case SortbyEnum.Name:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                break;
            case SortbyEnum.Country:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Country) : query.OrderBy(i => i.Country);
                break;
            default:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                break;
        }
    }
}
