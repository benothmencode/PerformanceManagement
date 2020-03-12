using System;

namespace ProjectF.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Challenge { get; set; }
        public string System { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ObtainDate { get; set; }
        public int UserProgression { get; set; }
        public string StartedAt { get; set; }
        public string BadgeDeadline { get; set; }
        public int BadgeCriteria { get; set; }

    }
}
