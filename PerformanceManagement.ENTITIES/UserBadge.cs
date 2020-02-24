using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class UserBadge
    {

        public Guid UserId { get; set; }
        public User User{ get; set; }
        public Guid BadgeId { get; set; }
        public Badge Badge { get; set; }

    }
}
