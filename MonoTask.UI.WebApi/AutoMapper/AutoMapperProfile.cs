using AutoMapper;
using MonoTask.Core.Entities.Entities;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.UI.WebApi.Models.ResponseModels;

namespace MonoTask.UI.WebApi.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //i: EF to POCO
        CreateMap<UserEntity, User>();
        CreateMap<VehicleBrandEntity, VehicleBrand>();
        CreateMap<VehicleModelEntity, VehicleModel>()
           .ForMember(m => m.Brand, a => a.MapFrom(s => s.VehicleBrand));

        //i: POCO to EF
        CreateMap<User, UserEntity>();
        CreateMap<VehicleBrand, VehicleBrandEntity>();
        CreateMap<VehicleModel, VehicleModelEntity>();

        //i:  POCO to ViewModel
        CreateMap<VehicleModel, VehicleModelVM>()
            .ForMember(m => m.BrandName, a => a.MapFrom(s => s.Brand == null ? "" : s.Brand.Name))
            .ForMember(m => m.BrandId, a => a.MapFrom(s => s.Brand.Id));
        CreateMap<VehicleBrand, VehicleBrandVM>();


        //i: ViewModel to POCO
        CreateMap<VehicleModelVM, VehicleModel>()
            .ForMember(m => m.BrandId, a => a.MapFrom(s => s.BrandId));
        CreateMap<VehicleBrandVM, VehicleBrand>();

    }


}
