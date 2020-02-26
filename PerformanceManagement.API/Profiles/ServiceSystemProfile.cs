using AutoMapper;
using PerformanceManagement.API.Models.SystemEntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Profiles
{
    public class ServiceSystemProfile : Profile
    {

        public ServiceSystemProfile()
        {
            CreateMap<ENTITIES.ServiceSystem, ServiceSystemEntityDto>();
            CreateMap<ServiceSystemEntityForCreation, ENTITIES.Resource>();



        }
    }
}
