using AutoMapper;
using PerformanceManagement.ENTITIES;
using ProjectF.ViewModels.BadgeEntityModel;

namespace ProjectF.Profiles
{
    public class BadgeProfile : Profile
    {
        public BadgeProfile()
        {

            CreateMap<Badge,BadgeViewModel>();
            CreateMap<BadgeViewModel, Badge>();


        }


    }
}
