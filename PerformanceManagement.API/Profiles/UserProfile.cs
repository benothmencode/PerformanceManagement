using AutoMapper;
using PerformanceManagement.API.Models;
using PerformanceManagement.API.Models.userEntityModel;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserEntityDto>();
            CreateMap<UserEntityDto, User>();
            CreateMap<UserEntityDtoForCreate, User>();
        }
        }
}
