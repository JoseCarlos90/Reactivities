using AutoMapper;
using Domain;

namespace Application.Activities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(destination => destination.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
                .ForMember(destination => destination.DisplayName, opt => opt.MapFrom(s => s.AppUser.DisplayName));
        }
    }
}