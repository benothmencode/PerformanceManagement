using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.HomeRepository
{
    public class HomeRepository: IHomeRepository
    {

        private readonly PerformanceManagementDBContext _context;


        public HomeRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IEnumerable<Event> GetAll()
        {
            return _context.Events;
        }

        public IEnumerable<DayEvent> GetAlldayevents()
        {
            return _context.DayEvents;
        }



        public IEnumerable<DayEvent> GetDayEvent(int? eventId)
        {
            return _context.DayEvents.Where(u => u.EventId == eventId).ToList();

        }

    }
}
