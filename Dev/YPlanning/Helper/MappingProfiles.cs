using AutoMapper;
using YPlanning.Dto;
using YPlanning.Models;

namespace YPlanning.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
        }
    }
}
