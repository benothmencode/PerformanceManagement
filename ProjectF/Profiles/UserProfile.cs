using AutoMapper;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.ModelsDTOS;
using System.Reflection;

namespace ProjectF.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntityDto>();
        }

    }
}
