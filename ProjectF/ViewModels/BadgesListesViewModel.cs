using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class BadgesListesViewModel
    {
        public IEnumerable<UserBadge> BadgesObtained { get; set; }
        public IEnumerable<UserBadge> BadgesInProgress { get; set; }
    }
}
