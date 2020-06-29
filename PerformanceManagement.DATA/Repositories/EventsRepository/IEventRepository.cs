using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;

namespace PerformanceManagement.DATA.Repositories.EventsRepository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        public void createeventeveryday();
        public bool verifybadge();
        public IEnumerable<DayEvent> DayEvents();
        public IEnumerable<Event> Eventsperday(DateTime dateoftoday);
        public String dayevents();
        public IEnumerable<DayEvent> getAllDayEventsForToday();
        //public DayEvent newdayevent();
        public void addevent();
        public List<DayEvent> returndayeventlist(DayEvent dv);
        public bool CreateDayEvent(DayEvent dv);
       

    }
}