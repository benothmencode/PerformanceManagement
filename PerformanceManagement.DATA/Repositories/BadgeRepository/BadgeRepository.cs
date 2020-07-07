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

        //public void populateJobIds(List<string> jobIds)
        //{

        //    //Serialize 
        //    string Temp = JsonConvert.SerializeObject(jobIds);
        //    File.WriteAllText("C:\\Users\\Wijden BEN OTHMEN\\source\\repos\\PerformanceManagement\\ProjectF\\BadgeJobs\\jobIds.json", Temp);
        //}
        //public List<string> getJobIds()
        //{
        //    var MyList = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("C:\\Users\\Wijden BEN OTHMEN\\source\\repos\\PerformanceManagement\\ProjectF\\BadgeJobs\\jobIds.json"));
        //    return MyList ;
        //}

        public Badge GetBadgeByJobId(string jobId)
        {
            return _context.Badges.FirstOrDefault(b => b.jobId == jobId);
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




        public int numberOfBadges()
        {
            return _context.Badges.Count();
        }

        public bool Create(int? SystemeId, Badge badge, int? TypevoteId)
        {
            var saved = 0;

            if (_context.Badges.Any(x => x.Title == badge.Title))
                throw new Exception("Badge \"" + badge.Title + "\" exists already ");
            if (SystemeId != null)
            {
                var Systeme = _context.Systemes.Where(s => s.Id == SystemeId).FirstOrDefault();
                badge.SystemeId = SystemeId;
                badge.Systeme = Systeme;
                _context.Badges.Add(badge);
                saved = _context.SaveChanges();
            }
            if (TypevoteId != null)
            {
                var voteType = _context.TypeVotes.Where(tv => tv.Id == TypevoteId).FirstOrDefault();

                badge.TypeVote = voteType;
                badge.TypeVoteId = TypevoteId;
                _context.Badges.Add(badge);
                saved = _context.SaveChanges();
            }
            var users = _context.Users.ToList();
            foreach(var user in users)
            {
                if (!_UserbadgeRepository.UserBadgeExist(user.Id, badge.Id, badge.Created))
                {
                    _UserbadgeRepository.CreateUserBadge(user.Id, badge.Id);
                    badge.LastCreation = badge.Created;
                }
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
           return (Badge)_context.Badges.Where(d => d.Created == DateTime.Today);
            

        }


    }
}