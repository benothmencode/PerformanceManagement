using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.BadgeRepository
{
   public interface IBadgeRepository
    {
        public IEnumerable<Badge> GetAll();
        public Badge GetBadgeById(int? badgeId);
        public IEnumerable<Badge> GetUserBadge(int userId);
        public bool Create(int SystemeId  , Badge badge , UserBadge userBadge);
        public List<UserBadge> GetUsersBadge(Badge badge);
        public List<UserBadge> GetUserBadges();
        public Badge GetBadgeByTitle(string title);
    }
}
