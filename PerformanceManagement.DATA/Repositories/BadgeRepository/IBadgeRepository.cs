using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.BadgeRepository
{
   public interface IBadgeRepository
    {
        public IEnumerable<Badge> GetAll();
        public Badge GetBadgeById(int? badgeId);
        public IEnumerable<Badge> GetUserBadge(int userId);
        public bool Create(int? SystemeId  , Badge badge , int? TypeVoteId);
        public Badge GetBadgeByTitle(string title);
        public void UpdateLastCreationDate(DateTime LastCreationDate , Badge badge);


    }
}
