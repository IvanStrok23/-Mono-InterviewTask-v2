﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MonoTask.Core.Interfaces.Services;
using MonoTask.Core.Models.Dtos;
using MonoTask.Core.Models.Helpers;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.Infrastructure.Data.Interfaces;
using MonoTask.Infrastructure.Data.StaticData;
namespace MonoTask.Application.Services.Vehicle;
using POCO = Core.Entities.Entities;

public class VehicleModelService : IVehicleModelService
{
    private readonly IDataContext _dbContext;
    private readonly IMapper _mapper;
    public VehicleModelService(IDataContext vehiclesDbContext, IMapper mapper)
    {
        _dbContext = vehiclesDbContext;
        _mapper = mapper;
    }

    public async Task<int> InsertModel(POCO.VehicleModel entity)
    {
        if (entity == null || entity.Id != 0)
        {
            return 0;
        }
        VehicleModelEntity mapped = _mapper.Map<VehicleModelEntity>(entity);
        mapped.VehicleBrand = _dbContext.VehicleBrands.Where(i => i.Id == entity.MakeId).FirstOrDefault();
        return await _dbContext.Insert(mapped);
    }

    public async Task<POCO.VehicleModel> GetModelById(int id)
    {
        var query = await _dbContext.VehicleModels.Where(m => m.Id == id).Include(m => m.VehicleBrand).FirstOrDefaultAsync();
        return _mapper.Map<POCO.VehicleModel>(query);
    }

    public async Task<List<POCO.VehicleModel>> GetModels(PagingParams pagingParams)
    {

        var query = _dbContext.VehicleModels.
            Include(m => m.VehicleBrand)
            .AsQueryable();

        sortByColumn(ref query, pagingParams.SortParams.SortBy, pagingParams.SortParams.SortOrder);
        searchByName(ref query, pagingParams.SortParams.SearchValue);
        return _mapper.Map<List<POCO.VehicleModel>>(await query.Skip((pagingParams.Page - 1) * 10).Take(10).ToListAsync());

    }

    public async Task<List<POCO.VehicleModel>> GetModelsByName(string searchValue)
    {

        var query = await _dbContext.VehicleModels.
            Include(m => m.VehicleBrand).
            OrderBy(i => i.Name).
            Where(i => i.Name.StartsWith(searchValue)).
            Take(10).ToListAsync();
        return _mapper.Map<List<POCO.VehicleModel>>(query);

    }

    public async Task<bool> UpdateModel(POCO.VehicleModel model)
    {
        VehicleModelEntity temp = _dbContext.VehicleModels.Where(i => i.Id == model.Id).FirstOrDefault();

        if (temp == null)
        {
            return false;
        }
        else
        {
            temp.Name = model.Name;
            temp.VehicleBrand = _dbContext.VehicleBrands.Where(i => i.Id == model.MakeId).FirstOrDefault();
            temp.Year = model.Year;
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }

    public async Task<bool> DeleteModel(int id)
    {
        var toRemove = await Task.Run(() => _dbContext.VehicleModels.FindAsync(id)).Result;
        if (toRemove == null)
        {
            return false;
        }
        return await _dbContext.Remove(toRemove);
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
            var existingMake = await _dbContext.VehicleBrands
                .FirstOrDefaultAsync(b => b.Name == make.Name);

            if (existingMake == null)
            {
                _dbContext.VehicleBrands.Add(make);
                await _dbContext.SaveChangesAsync();
                existingMake = make;
            }

            var models = testData.GetHardCodedModelsByMakeName(make.Name, existingMake.Id);

            foreach (var model in models)
            {
                var existingModel = await _dbContext.VehicleModels
                    .FirstOrDefaultAsync(m => m.Name == model.Name && m.Year == model.Year && m.VehicleBrandId == existingMake.Id);

                if (existingModel == null)
                {
                    model.VehicleBrandId = existingMake.Id;
                    _dbContext.VehicleModels.Add(model);
                }
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
