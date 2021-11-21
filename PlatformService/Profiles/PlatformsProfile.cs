using AutoMapper;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreatDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishDto>();
            CreateMap<Platform, GrpcPlatformModel>()
            .ForMember(d => d.PlatformId, opt => opt.MapFrom(src => src.Id))
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(d=>d.Publisher,opt=>opt.MapFrom(src=>src.Publisher));
        }
    }
}