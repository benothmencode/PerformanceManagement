using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class UserBadge
    {

        public int Id { get; set; }

        public int UserProgression { get; set; }
        public string State { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? LastUpdate { get; set; }

        public DateTime? ObtainedAt { get; set; }
        public DateTime? BadgeDeadline { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("BadgeId")]
        public int BadgeId { get; set; }
        public User User { get; set; }
        public Badge Badge { get; set; }


    }
}
