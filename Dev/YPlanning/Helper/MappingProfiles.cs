using AutoMapper;
using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}
