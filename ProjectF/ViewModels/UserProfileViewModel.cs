using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class UserProfileViewModel
    {
        public UserEntityDto user { get; set; }
        public IList<UserBadge> badgesObtained { get; set; }
        public IList<UserBadge> badgesInProgress { get; set; }
        public IList<VoteHistory> VoteHistories { get; set; }
    }
}
