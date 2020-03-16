using AutoMapper;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels.BadgeEntityModel;

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
