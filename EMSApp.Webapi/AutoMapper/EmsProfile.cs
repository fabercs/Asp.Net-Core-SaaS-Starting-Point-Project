using AutoMapper;
using EMSApp.Core.DTO;
using EMSApp.Core.Entities;
using System.Linq;

namespace EMSApp.Webapi.AutoMapper
{
    public class EmsProfile : Profile
    {
        public EmsProfile()
        {
            CreateMap<FairCreateDto, Fair>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Fair, FairListDto>();
            CreateMap<Fair, FairDetailDto>()
                .ForMember(dest => dest.FirmList, opt => opt.MapFrom(src=> src.FairFirm.Select(x=> x.Firm)));

            CreateMap<FirmCreateDto, Firm>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Firm, FirmListDto>()
                .ForMember(dest => dest.FirmStatus, opt => opt.MapFrom(src => src.FirmStatus.ToString()))
                .ForMember(dest => dest.FirmType, opt => opt.MapFrom(src => src.FirmType.ToString()));
            CreateMap<Firm, FirmDetailDto>()
                .ForMember(dest => dest.FirmStatus, opt => opt.MapFrom(src => src.FirmStatus.ToString()))
                .ForMember(dest => dest.FirmType, opt => opt.MapFrom(src => src.FirmType.ToString()))
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(dest => dest.Fairs, opt => opt.MapFrom(src => src.FairFirm.Select(x => x.Fair)));

            CreateMap<FirmContactCreateDto, FirmContact>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<FirmContact, FirmContactListDto>();
            CreateMap<FirmContact, FirmContactDetailDto>();
        }

    }
}
