using AutoMapper;
using MonoTask.Core.Entities.Entities;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.UI.WebApi.Models;

namespace MonoTask.UI.WebApi.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //i: EF to POCO
        CreateMap<VehicleBrandEntity, VehicleBrand>();
        CreateMap<VehicleModelEntity, VehicleModel>()
           .ForMember(m => m.VehicleBrand, a => a.MapFrom(s => s.VehicleBrand));

        //i: POCO to EF
        CreateMap<VehicleBrand, VehicleBrandEntity>();
        CreateMap<VehicleModel, VehicleModelEntity>();

        //i:  POCO to ViewModel
        CreateMap<VehicleModel, VehicleModelVM>()
            .ForMember(m => m.BrandName, a => a.MapFrom(s => s.VehicleBrand == null ? "" : s.VehicleBrand.Name))
            .ForMember(m => m.BrandId, a => a.MapFrom(s => s.VehicleBrand.Id));
        CreateMap<VehicleBrand, VehicleBrandVM>();


        //i: ViewModel to POCO
        CreateMap<VehicleModelVM, VehicleModel>()
            .ForMember(m => m.MakeId, a => a.MapFrom(s => s.BrandId));
        CreateMap<VehicleBrand, VehicleBrandVM>();

    }


}
