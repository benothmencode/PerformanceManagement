using ProjectF.ModelsDTOS;
using System;

namespace ProjectF.ViewModels
{
    public class Event
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string icon { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public UserEntityDto User { get; set; }

        public string Action { get; set; }
        public string Description { get; set; }

    }
}
