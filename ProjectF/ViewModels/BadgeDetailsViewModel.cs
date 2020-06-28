using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class BadgeDetailsViewModel
    {
        public BadgeEntityDto badgeEntityDto { get; set; }
        public IList<User> usersVotedForMe { get; set; }
    }
}
