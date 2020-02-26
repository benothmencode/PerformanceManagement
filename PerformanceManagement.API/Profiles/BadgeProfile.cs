using AutoMapper;
using PerformanceManagement.API.Models.badgeEntityModel;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Profiles
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
