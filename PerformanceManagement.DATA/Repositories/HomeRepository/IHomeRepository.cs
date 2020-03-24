using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.HomeRepository
{
   public  interface IHomeRepository
    {

        public IEnumerable<Event> GetAll();
        public IEnumerable<DayEvent> GetAlldayevents();
        public IEnumerable<DayEvent> GetDayEvent(int? eventId);
    }
}
