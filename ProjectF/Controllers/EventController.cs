using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.ENTITIES;

namespace ProjectF.Controllers
{
    [Route("/[controller]/[action]")]
    public class EventController : Controller
    {
        
        private readonly IMapper _mapper;
        private  EventRepository _EventRepository;

        public EventController(IMapper mapper,
            EventRepository eventRepository, IWebHostEnvironment webHostEnvironment)
        {


            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _EventRepository = eventRepository ??
                throw new ArgumentNullException(nameof(eventRepository));

        }

       
        
        public IEnumerable<Event> GetEvents()
        {
            var events = _EventRepository.GetAll();
            return events;
        }
       DateTime date = DateTime.Today;
        public IEnumerable<Event> Eventsperday(DateTime date )
        {
           return _EventRepository.Eventsperday(date);
        }


        public String dayevents()
        {
            return _EventRepository.dayevents();
        }

        [ActionName("DayeventsPerDate")]
        public IEnumerable<DayEvent> DayeventsPerDate()
        {
            return _EventRepository.getAllDayEventsPerDate();
        }


    }
}