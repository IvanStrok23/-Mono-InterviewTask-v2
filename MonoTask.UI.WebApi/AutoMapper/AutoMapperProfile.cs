using AutoMapper;
using MonoTask.Infrastructure.Data.Entities;
using MonoTask.UI.WebApi.Models;

namespace MonoTask.UI.WebApi.AutoMapper;
using POCO = Core.Entities.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //i: EF to POCO
        CreateMap<VehicleBrandEntity, POCO.VehicleBrand>();
        CreateMap<VehicleModelEntity, POCO.VehicleModel>()
           .ForMember(m => m.VehicleBrand, a => a.MapFrom(s => s.VehicleBrand));

        //i: POCO to EF
        CreateMap<POCO.VehicleBrand, VehicleBrandEntity>();
        CreateMap<POCO.VehicleModel, VehicleModelEntity>();

        //i:  POCO to ViewModel
        CreateMap<POCO.VehicleModel, VehicleModelVM>()
            .ForMember(m => m.MakeName, a => a.MapFrom(s => s.VehicleBrand == null ? "" : s.VehicleBrand.Name))
            .ForMember(m => m.MakeId, a => a.MapFrom(s => s.VehicleBrand.Id));
        CreateMap<POCO.VehicleBrand, VehicleBrandVM>();


        //i: ViewModel to POCO
        CreateMap<VehicleModelVM, POCO.VehicleModel>()
            .ForMember(m => m.MakeId, a => a.MapFrom(s => s.MakeId));
        CreateMap<POCO.VehicleBrand, VehicleBrandVM>();

    }


}
