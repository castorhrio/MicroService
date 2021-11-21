using CommandService.Dtos;
using CommandService.Models;
using AutoMapper;
using PlatformService;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command,CommandReadDto>();
            CreateMap<PlatformPublishDto,Platform>()
                .ForMember(dest=>dest.ExtendID,opt=>opt.MapFrom(src=>src.Id));
            CreateMap<GrpcPlatformModel,Platform>()
                .ForMember(dest=>dest.ExtendID,opt=>opt.MapFrom(src=>src.PlatformId))
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest=>dest.Commands,opt=>opt.Ignore());
        }
    }
}