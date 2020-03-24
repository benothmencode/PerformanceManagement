using AutoMapper;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Profiles
{
    public class EventProfile :Profile
    {

        public EventProfile()
        {

            CreateMap<Event, EventEntityDto>();


        }


    }
}
