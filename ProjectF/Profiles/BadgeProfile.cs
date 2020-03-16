using AutoMapper;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;

namespace ProjectF.Profiles
{
    public class BadgeProfile : Profile
    {
        public BadgeProfile()
        {

            CreateMap<Badge, BadgeEntityDto>();
            CreateMap<BadgeEntityDto, Badge>();


        }


    }
}
