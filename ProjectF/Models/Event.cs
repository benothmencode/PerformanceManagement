using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Models
{
    public class Event
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public List<DayEvent> DayEvents { get; set; }




    }
}
