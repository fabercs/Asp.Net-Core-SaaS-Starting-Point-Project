using AutoMapper;
using EMSApp.Core.DTO;
using EMSApp.Core.Entities;

namespace EMSApp.Webapi.AutoMapper
{
    public class EmsProfile : Profile
    {
        public EmsProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Tenant, opt => opt.MapFrom(src => src.Tenant));

            CreateMap<Tenant, TenantDto>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.TenantSetting.Language))
                .ForMember(dest => dest.DateTimeZone, opt => opt.MapFrom(src => src.TenantSetting.DatetimeZone));
          
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
                .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.Name))
                .ForMember(dest => dest.Actions, opt => opt.MapFrom(src => src.Actions));

            CreateMap<Action, ActionDto>();

        }

    }
}
