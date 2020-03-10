using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public DateTime ObtainDate { get; set; }
        public int UserProgression { get; set; }
        public string StartedAt { get; set; }
        public string BadgeDeadline { get; set; }
        public int BadgeCriteria { get; set; }

    }
}
