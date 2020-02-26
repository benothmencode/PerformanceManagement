using AutoMapper;
using PerformanceManagement.API.Models.SystemEntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Profiles
{
    public class SystemProfile : Profile
    {

        public SystemProfile()
        {
            CreateMap<ENTITIES.ServiceSystem, SystemEntityDto>();
            CreateMap<SystemEntityForCreation, ENTITIES.Resource>();



        }
    }
}
