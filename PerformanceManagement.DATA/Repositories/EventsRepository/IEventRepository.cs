using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;

namespace PerformanceManagement.DATA.Repositories.EventsRepository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
         IEnumerable<DayEvent> DayEvents();
        public IEnumerable<Event> Eventsperday(DateTime dateoftoday);
        public String dayevents();
    }
}