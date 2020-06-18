using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.EventsRepository
{
    public class EventRepository : IEventRepository
    {


        private PerformanceManagementDBContext _context;

        private DateTime dateoftoday ;
        public EventRepository(PerformanceManagementDBContext context)
        {
            dateoftoday =  DateTime.Today;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }




        public IEnumerable<DayEvent> DayEvents()
        {
          var  events=  _context.Events;
           
            IEnumerable<DayEvent> dayevents =null;
            foreach(var day in events)
            {
                dayevents = day.DayEvent;
            }
            return dayevents;
        }

        public IEnumerable<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        
        public IEnumerable<Event> Eventsperday(DateTime dateoftoday)
        {
           
            var events = _context.Events.Where(d => d.Date == dateoftoday);

            return events;

        }


        public IEnumerable<DayEvent> getAllDayEventsPerDate()
        {

            IEnumerable<Event> events = _context.Events.ToList();
            IEnumerable<DayEvent> dayevents = _context.DayEvents.ToList();
          
            foreach (var ev in events)
            {
                int idEvent = ev.Id;
                foreach (var dv in dayevents)
                {
                   int  idDayEvent = dv.EventId;

                    if (idEvent == idDayEvent)
                    {
                        return dayevents;
                    }
                    
                }
             
            }
            return dayevents;
        }


        public String dayevents()
        {

            IEnumerable<DayEvent> dayevents = _context.DayEvents;
            IEnumerable<Event> events=_context.Events;
         
            String titreevent = null;
           DateTime date = new DateTime();
           foreach(var ev in events)
            {
                date = ev.Date;
                if(date == DateTime.Today)
                {
                    foreach(var dv in dayevents)
                    {
                       titreevent= dv.Title;
                    }

                  
                }
             
            }
            return titreevent;
        }


    }
}
