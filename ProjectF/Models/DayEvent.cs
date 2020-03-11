using System;

namespace ProjectF.Models
{
    public class DayEvent
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string icon { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public User User { get; set; }

        public string Action { get; set; }
        public string Description { get; set; }

    }
}
