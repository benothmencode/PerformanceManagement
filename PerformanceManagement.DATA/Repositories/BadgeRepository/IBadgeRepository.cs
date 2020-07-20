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
        public bool Create(int? SystemeId, Badge badge, int? TypevoteId);

        public int numberOfBadges();
        public void UpdateLastCreationDate(DateTime LastCreationDate, Badge badge);
        public Badge GetBadgeByTitle(string title);
        public Badge BadgeForToday();
        public bool badgefortoday(DateTime d);

        public List<Badge> GetBadgesByJobId(string jobId);
        public void DesactivateBadge(int idbadge , bool result);
        public bool update(Badge badge);

        public VoteRights GetVoteRights(User user1, DateTime date);
    }
}