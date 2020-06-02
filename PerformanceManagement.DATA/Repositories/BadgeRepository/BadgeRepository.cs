using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.BadgeRepository
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly PerformanceManagementDBContext _context;


        public BadgeRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public IEnumerable<Badge> GetAll()
        {
            return _context.Badges.Include(b => b.Systeme).ToList();
        }

       
        public IEnumerable<Badge> GetUserBadge(int userId)
        {
            return _context.userBadges.Where(u => u.UserId == userId).Select(b => b.Badge).ToList();

        }

        public Badge GetBadgeById(int? badgeId)
        {
            return _context.Badges.Include(b => b.UserBadges).Where(b => b.Id == badgeId).FirstOrDefault();
        }






        public bool Create(int SystemeId , Badge badge , UserBadge userBadge)
        {
            if (_context.Badges.Any(x => x.Title == badge.Title))
                throw new Exception("Badge \"" + badge.Title + "\" exists already ");
            var Systeme = _context.Systemes.Where(s => s.Id == SystemeId).FirstOrDefault();
            badge.SystemeId = SystemeId;
            badge.Systeme = Systeme;
            var Users = _context.Users.ToList();
            foreach(var user in Users)
            {
                var UserB = new UserBadge()
                {
                    Badge = badge,
                    User = user,
                    BadgeDeadline = userBadge.BadgeDeadline,
                    StartedAt = badge.Created,
                    LastUpdate = badge.Created
                };
                _context.Add(UserB);
            }
          _context.Badges.Add(badge);
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public void Update(Badge badgeParam/*, string password = null*/)
        {
           var badge = _context.Badges.Find(badgeParam.Id);
           if (badge == null)
               throw new Exception("Badge does not exist");

        
        // update Badge if it has changed
            if (!string.IsNullOrWhiteSpace(badgeParam.Title) && badgeParam.Title != badge.Title)
        {
            // throw error if the new badge is already taken
              if (_context.Badges.Any(x => x.Title == badgeParam.Title))
               throw new Exception("the Title of the badge  " + badgeParam.Title + " is already taken");

             badge.Title = badgeParam.Title;
           }
           // update badge properties if provided
           if (!string.IsNullOrWhiteSpace(badgeParam.Description))
              badge.Description = badgeParam.Description;


                badge.BadgeCriteria = badgeParam.BadgeCriteria;

            _context.Badges.Update(badge);
            _context.SaveChanges();


        }

        public void Delete(Guid BadgeId)
        {
           var badge = _context.Badges.Find(BadgeId);
            if (badge != null)
            {
                _context.Badges.Remove(badge);
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

        public Badge GetBadgeByTitle(string title)
        {
            return _context.Badges.Where(b => b.Title == title).FirstOrDefault();
        }


    }
}