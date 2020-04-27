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
            return _context.userBadges.Where(u => u.UserId == userId).Where(ub => DateTime.Compare(ub.StartedAt, ub.BadgeDeadline) < 0).Select(b => b.Badge).ToList();

        }

        public Badge GetBadgeById(int? badgeId)
        {
            return _context.Badges.Include(b => b.UserBadges).Where(b => b.Id == badgeId).FirstOrDefault();
        }






        //public void Create(Badge badge)
        //{
        //    if (_context.Badges.Any(x => x.Title == badge.Title))
        //        throw new Exception("Badge \"" + badge.Title + "\" exists already ");

        //    badge.Id = 1;
        //    badge.Title = badge.Title;
        //    badge.Description = badge.Description;
        //    badge.BadgesCriteria = badge.BadgesCriteria;
        //    badge.Icon = badge.Icon;

        //    _context.Badges.Add(badge);
        //    _context.SaveChanges();

        //}

        //public void Update(Badge badgeParam/*, string password = null*/)
        //{
        //    var badge = _context.Badges.Find(badgeParam.Id);
        //    if (badge == null)
        //        throw new Exception("Badge does not exist");

        //    // update Badge if it has changed
        //    if (!string.IsNullOrWhiteSpace(badgeParam.Title) && badgeParam.Title != badge.Title)
        //    {
        //        // throw error if the new badge is already taken
        //        if (_context.Badges.Any(x => x.Title == badgeParam.Title))
        //            throw new Exception("the Title of the badge  " + badgeParam.Title + " is already taken");

        //        badge.Title = badgeParam.Title;
        //    }
        //    // update badge properties if provided
        //    if (!string.IsNullOrWhiteSpace(badgeParam.Description))
        //        badge.Description = badgeParam.Description;


        //        badge.BadgesCriteria = badgeParam.BadgesCriteria;

        //    _context.Badges.Update(badge);
        //    _context.SaveChanges();


        //}

        //public void Delete(Guid BadgeId)
        //{
        //    var badge = _context.Badges.Find(BadgeId);
        //    if (badge != null)
        //    {
        //        _context.Badges.Remove(badge);
        //        _context.SaveChanges();
        //    }
        //}


    }
}