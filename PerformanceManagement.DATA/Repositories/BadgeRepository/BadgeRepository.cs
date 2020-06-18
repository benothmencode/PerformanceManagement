using Microsoft.AspNetCore.Identity;
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

        private readonly IVoteRepository _VoteRepository;

        private readonly UserManager<User> _userManager;


        public BadgeRepository(PerformanceManagementDBContext context, UserManager<User> userManager , IVoteRepository voteRepository )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _VoteRepository = voteRepository ?? throw new ArgumentNullException(nameof(voteRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }



        public IEnumerable<Badge> GetAll()
        {
            return _context.Badges.Include(b => b.Systeme).Include(b => b.TypeVote).ToList();
        }

       
        public IEnumerable<Badge> GetUserBadge(int userId)
        {
            return _context.userBadges.Where(u => u.UserId == userId).Select(b => b.Badge).ToList();

        }

        public Badge GetBadgeById(int? badgeId)
        {
            return _context.Badges.Include(b => b.UserBadges).Where(b => b.Id == badgeId).FirstOrDefault();
        }




        public int numberOfBadges()
        {
           return _context.Badges.Count();
        }

        public bool Create(int? SystemeId , Badge badge , int? TypevoteId )
        {
            var saved = 0;
            
            if (_context.Badges.Any(x => x.Title == badge.Title))
                throw new Exception("Badge \"" + badge.Title + "\" exists already ");
            badge.LastCreation = badge.Created;
            if (SystemeId != null)
            {
                var Systeme = _context.Systemes.Where(s => s.Id == SystemeId).FirstOrDefault();
                badge.SystemeId = SystemeId;
                badge.Systeme = Systeme;
                _context.Badges.Add(badge);
                 saved = _context.SaveChanges();
                return saved >= 0 ? true : false;
            }
            if (TypevoteId != null)
            {
                var voteType = _context.TypeVotes.Where(tv => tv.Id == TypevoteId).FirstOrDefault();
              
                badge.TypeVote = voteType;
                badge.TypeVoteId = TypevoteId;
                _context.Badges.Add(badge);
                 saved = _context.SaveChanges();
                return saved >= 0 ? true : false;
            }
            return saved >= 0 ? true : false; ;
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



        public Badge GetBadgeByTitle(string title)
        {
            return _context.Badges.Where(b => b.Title == title).FirstOrDefault();
        }

        public void UpdateLastCreationDate(DateTime LastCreationDate , Badge badge )
        {
            if(LastCreationDate == null)
                throw new Exception("LastCreationDate \"" + LastCreationDate + "\"is NULL ");

           var badgefound = _context.Badges.Find(badge.Id);
            badgefound.LastCreation = LastCreationDate;
            _context.Update(badgefound);
            _context.SaveChanges();
        }

    }
}