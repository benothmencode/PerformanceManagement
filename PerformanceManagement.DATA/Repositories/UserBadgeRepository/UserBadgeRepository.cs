using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.UserBadgeRepository
{
    public class UserBadgeRepository : IUserBadgeRepository
    {
        private readonly PerformanceManagementDBContext _context;


        public UserBadgeRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<UserBadge> GetUsersBadge(Badge badge)
        {
            return _context.userBadges.Where(ub => ub.Badge == badge).ToList();
        }

        public List<UserBadge> GetUserBadges()
        {
            return _context.userBadges.ToList();
        }
        public UserBadge GetUserBadge(int UserId, int BadgeId)
        {
            return _context.userBadges.Where(u => u.BadgeId == BadgeId).Where(u => u.UserId == UserId).Include(ub => ub.User).Include(ub => ub.Badge).Include(ub => ub.Progressions).FirstOrDefault();
        }
        public bool UpdateProgression(Progression progression)
        {
            _context.Update(progression);
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public Progression GetLastProgressionofUserbadge(int ubID)
        {
            var progression = _context.Progressions.Where(p => p.UserBadgeId == ubID).ToList().LastOrDefault();
            if (progression != null)
            {
               
                return progression;
            }
            return null;
        }

    }
}
