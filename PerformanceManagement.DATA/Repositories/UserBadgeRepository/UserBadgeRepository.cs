using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void CreateUserBadge(int idUser , int idBadge)
        {

            var badgedeadline = new DateTime();
            var user = _context.Users.Find(idUser);
            var badge = _context.Badges.Find(idBadge);
            Periodicity weekly = Periodicity.Weekly;
            Periodicity Monthly = Periodicity.Monthly;
            Periodicity Yeary = Periodicity.Yearly;

            if (badge.periodicity.Equals(weekly))
            {
                 badgedeadline = badge.Created.AddDays(7 * badge.ValueOfPeriodicity); 
            }
            else if(badge.periodicity.Equals(Monthly)) {
                 badgedeadline = badge.Created.AddMonths(badge.ValueOfPeriodicity);
            }
            else if(badge.periodicity.Equals(Yeary))
            {
                 badgedeadline = badge.Created.AddYears(badge.ValueOfPeriodicity);
            }
            if (!UserBadgeExist(idUser, idBadge, badge.Created))
            {
                var userBadge = new UserBadge()
                {
                    Badge = badge,
                    User = user,
                    StartedAt = badge.Created,
                    State = "In progress",
                    BadgeDeadline = badgedeadline,

                };
                _context.Add(userBadge);
                _context.SaveChanges();
            }
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

        //UserBadgeExistAtcertainTime
        public bool UserBadgeExist(int idUser , int idBadge , DateTime Date)
        {
            var result = false;
            var userBadge = _context.userBadges.Where(ub => ub.UserId == idUser).Where(ub => ub.BadgeId == idBadge).Where(ub => ub.StartedAt == Date).FirstOrDefault();
            if (userBadge != null)
            {
                result = true;
                return result;
            }
            return result;
        }
    }
}
