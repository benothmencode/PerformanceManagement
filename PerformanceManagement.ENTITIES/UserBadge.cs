using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class UserBadge
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }


        public int UserProgression { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? ObtainedAt { get; set; }
        public DateTime BadgeDeadline { get; set; }


        public User User { get; set; }
        public Badge Badge { get; set; }
    }
}
