using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PerformanceManagement.API.Models.ResourceEntityModel;

namespace PerformanceManagement.API.Profiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<ENTITIES.Resource, ResourceEntityDto>();
            CreateMap<ResourceEntityForCreation, ENTITIES.Resource>();



        }
    }
}
