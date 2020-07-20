using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.BadgeRepository
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly PerformanceManagementDBContext _context;

        //private readonly IVoteRepository _VoteRepository;

        private readonly UserManager<User> _userManager;

        private readonly IUserBadgeRepository _UserbadgeRepository;

        // This is a hack, only used at runtime
        private List<string> _jobIds;

        public BadgeRepository(PerformanceManagementDBContext context, UserManager<User> userManager, IUserBadgeRepository userBadgeRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_VoteRepository = voteRepository ?? throw new ArgumentNullException(nameof(voteRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _UserbadgeRepository = userBadgeRepository;

            _jobIds  = new List<string>();
        }


        public List<Badge> GetBadgesByJobId(string jobId)
        {
            return _context.Badges.Where(b => b.jobId == jobId).ToList();
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
            return _context.Badges.Include(b => b.UserBadges).Include(b => b.Systeme).Include(b=> b.TypeVote).Include(b=> b.voteHistories).Where(b => b.Id == badgeId).FirstOrDefault();
        }

        public VoteRights GetVoteRights(User user1 , DateTime date )
        {
            var voterights = _context.VoteRights.Where(vr => vr.UserId == user1.Id).ToList();
            foreach(var vr in voterights)
            {
              var date1 =  vr.Update.ToString("MM/dd/yyyy hh:mm:ss");
               var date2 =  date.ToString("MM/dd/yyyy hh:mm:ss");

                    if (date1 == date2)
                    {
                        return vr;
                    }
              

            }
            return null;
        }


        public int numberOfBadges()
        {
            return _context.Badges.Count();
        }

       

       


        public Badge GetBadgeByTitle(string title)
        {
            return _context.Badges.Where(b => b.Title == title).FirstOrDefault();
        }

        public void UpdateLastCreationDate(DateTime LastCreationDate, Badge badge)
        {
            if (LastCreationDate == null)
                throw new Exception("LastCreationDate \"" + LastCreationDate + "\"is NULL ");

            var badgefound = _context.Badges.Find(badge.Id);
            badgefound.LastCreation = LastCreationDate;
            _context.Update(badgefound);
            _context.SaveChanges();
        }

      
        public bool badgefortoday(DateTime d)
        {
            d = DateTime.Today;
            var bad = BadgeForToday();
            if (bad.Created == d)
            {
                return true;
            }
            else return false;
            
        }

        public Badge BadgeForToday()
        {
           return _context.Badges.Where(d => d.Created == DateTime.Today).FirstOrDefault();
            

        }


        public void DesactivateBadge(int idbadge , bool result)
        {
            var badge = _context.Badges.FirstOrDefault(b => b.Id == idbadge);
            if(badge != null)
            {
                badge.IsArchieved = result;
                _context.Badges.Update(badge);
                _context.SaveChanges();
            }
        }

        public bool Create(int? SystemeId, Badge badge, int? TypevoteId)
        {
            var saved = 0;

            if (_context.Badges.Any(x => x.Title == badge.Title))
                throw new Exception("Badge \"" + badge.Title + "\" exists already ");

            badge.Created = DateTime.Now;
            if (SystemeId != null)
            {
                var Systeme = _context.Systemes.Where(s => s.Id == SystemeId).FirstOrDefault();
                badge.SystemeId = SystemeId;
                badge.Systeme = Systeme;
                badge.IsArchieved = false;
                badge.SystemIsArchieved = false;
                _context.Badges.Add(badge);
                saved = _context.SaveChanges();
            }
            if (TypevoteId != null)
            {
                var voteType = _context.TypeVotes.Where(tv => tv.Id == TypevoteId).FirstOrDefault();

                badge.TypeVote = voteType;
                badge.TypeVoteId = TypevoteId;
                badge.IsArchieved = false;
                _context.Badges.Add(badge);
                saved = _context.SaveChanges();
            }
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (!_UserbadgeRepository.UserBadgeExist(user.Id, badge.Id, badge.Created))
                {
                    _UserbadgeRepository.CreateUserBadge(user.Id, badge.Id);
                    badge.LastCreation = badge.Created;
                }
            }
            return saved >= 0 ? true : false;
        }



        public bool update(Badge badge)
        {
            var saved = 0;
            if (_context.Badges.Where(b => b.Id != badge.Id).Any(x => x.Title == badge.Title))
                throw new Exception("Badge \"" + badge.Title + "\" exists already ");
              badge.IsArchieved = false;
                _context.Badges.Update(badge);
                saved = _context.SaveChanges();
            return saved >= 0 ? true : false;



        }




    }
}