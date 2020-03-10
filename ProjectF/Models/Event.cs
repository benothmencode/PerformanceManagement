using System;
using System.Collections.Generic;

namespace ProjectF.Models
{
    public class Event
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public List<DayEvent> DayEvents { get; set; }




    }
}
