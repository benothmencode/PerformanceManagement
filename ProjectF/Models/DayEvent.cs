using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Models
{
    public class DayEvent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string icon { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public User  User{ get; set; }

        public string Action { get; set; }
        public string Description { get; set; }

    }
}
