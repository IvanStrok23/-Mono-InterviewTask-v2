using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonoTask.Core.Entities.Entities;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Core.Models.Dtos;
using MonoTask.Core.Models.Helpers;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.Infrastructure.Data.StaticData;
namespace MonoTask.Application.Services.Vehicle;
public class VehicleModelService : IVehicleModelService
{
    private readonly IDataContext _context;
    private readonly IMapper _mapper;
    public VehicleModelService(IDataContext vehiclesDbContext, IMapper mapper)
    {
        _context = vehiclesDbContext;
        _mapper = mapper;
    }

    public async Task<int> InsertModel(VehicleModel entity)
    {
        if (entity == null || entity.Id != 0)
        {
            return 0;
        }
        VehicleModelEntity mapped = _mapper.Map<VehicleModelEntity>(entity);
        mapped.VehicleBrand = _context.VehicleBrands.Where(i => i.Id == entity.BrandId).FirstOrDefault();
        return await _context.Insert(mapped);
    }

    public async Task<VehicleModel> GetModelById(int id)
    {
        var query = await _context.VehicleModels.Where(m => m.Id == id).Include(m => m.VehicleBrand).FirstOrDefaultAsync();
        return _mapper.Map<VehicleModel>(query);
    }

    public async Task<List<VehicleModel>> GetModels(PagingParams pagingParams)
    {

        var query = _context.VehicleModels.
            Include(m => m.VehicleBrand)
            .AsQueryable();

        sortByColumn(ref query, pagingParams.SortParams.SortBy, pagingParams.SortParams.SortOrder);
        searchByName(ref query, pagingParams.SortParams.SearchValue);
        return _mapper.Map<List<VehicleModel>>(await query.Skip((pagingParams.Page - 1) * 10).Take(10).ToListAsync());

    }

    public async Task<bool> UpdateModel(VehicleModel model)
    {
        VehicleModelEntity temp = _context.VehicleModels.Where(i => i.Id == model.Id).FirstOrDefault();

        if (temp == null)
        {
            return false;
        }
        else
        {
            temp.Name = model.Name;
            temp.VehicleBrand = _context.VehicleBrands.Where(i => i.Id == model.BrandId).FirstOrDefault();
            temp.Year = model.Year;
            await _context.SaveChangesAsync();
            return true;
        }

    }

    public async Task<bool> DeleteModel(int id)
    {
        var toRemove = await Task.Run(() => _context.VehicleModels.FindAsync(id)).Result;
        if (toRemove == null)
        {
            return false;
        }
        return await _context.Remove(toRemove);
    }

    private void sortByColumn(ref IQueryable<VehicleModelEntity> query, SortbyEnum sortBy, SortOrderEnum sortOrder)
    {

        switch (sortBy)
        {
            case SortbyEnum.Name:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                break;
            case SortbyEnum.MakeName:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.VehicleBrand.Name) : query.OrderBy(i => i.VehicleBrand.Name);
                break;
            case SortbyEnum.Year:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Year) : query.OrderBy(i => i.Year);
                break;
            default:
                query = sortOrder == SortOrderEnum.Desc ? query.OrderByDescending(i => i.Name) : query.OrderBy(i => i.Name);
                break;
        }
    }

    private void searchByName(ref IQueryable<VehicleModelEntity> query, string searchValue)
    {

        if (!String.IsNullOrWhiteSpace(searchValue))
        {
            query = query.Where(i => i.Name.StartsWith(searchValue));
        }
    }

    public async Task AddTestData()
    {
        TestData testData = new TestData();
        var makes = testData.MakeList;

        foreach (var make in makes)
        {
            var existingMake = await _context.VehicleBrands
                .FirstOrDefaultAsync(b => b.Name == make.Name);

            if (existingMake == null)
            {
                _context.VehicleBrands.Add(make);
                await _context.SaveChangesAsync();
                existingMake = make;
            }

            var models = testData.GetHardCodedModelsByMakeName(make.Name, existingMake.Id);

            foreach (var model in models)
            {
                var existingModel = await _context.VehicleModels
                    .FirstOrDefaultAsync(m => m.Name == model.Name && m.Year == model.Year && m.VehicleBrandId == existingMake.Id);

                if (existingModel == null)
                {
                    model.VehicleBrandId = existingMake.Id;
                    _context.VehicleModels.Add(model);
                }
            }
        }

        await _context.SaveChangesAsync();
    }
}
