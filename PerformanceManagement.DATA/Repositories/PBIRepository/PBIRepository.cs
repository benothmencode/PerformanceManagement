using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.PBIRepository
{
    public class PBIRepository : IPBIRepository
    {
    

        private PerformanceManagementDBContext _context;

        private IBadgeRepository _badgeRepository;
        private IUserRepository _userRepository;
        private IUserBadgeRepository _userBadgeRepository;
        public PBIRepository(PerformanceManagementDBContext context, IUserBadgeRepository userBadgeRepository, IBadgeRepository badgerepository, IUserRepository userRepository)
        {

            _userRepository = userRepository;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _badgeRepository = badgerepository;
            _userBadgeRepository = userBadgeRepository;

        }





        public void CreatePBI()
        {
         var useb=  _userBadgeRepository.GetUserBadges().Distinct();
            var bd = _userBadgeRepository.GetAll().Distinct();

            foreach (var b in bd)
            {
                var badge = _badgeRepository.GetBadgeById(b.BadgeId);
               var user = _userRepository.GetUserById(b.UserId);
                PBIEntity pbi = new PBIEntity();

                pbi.Id = new Int32();
                pbi.BadgeTitle = badge.Title;
                pbi.Username =user.UserName;
                pbi.UserProgression = b.UserProgression;


                _context.Add(pbi);
                _context.SaveChanges();
            }
                
            }
            }
        }
         
    

            //var badge = _context.Badges.Where(b => b.Title == Title).FirstOrDefault();
           //var userOwner = _context.Users.FirstOrDefault(u => u.Id == UserId);
            //var userChosen = _context.Users.FirstOrDefault(u => u.Id == idUserChosen);


            //        var badges = _badgeRepository.GetAll();
            //        var users = _userRepository.GetUsers();

            //        foreach (var u in users) {
            //            foreach (var b in badges)
            //            {
            //                var title = b.Title;
            //               var badge = _badgeRepository.GetBadgeByTitle(title);
            //                var badgee =_badgeRepository.GetUserBadge(u.Id);
            //                PBIEntity pbi = new PBIEntity()
            //                {
            //                    BadgeTitle = title,

            //                }


            //        }
            //        }


            //        PBIEntity pbi = new PBIEntity()
            //        {
            //            BadgeTitle=badge.Title,
            //            UserOwnerId = UserId,
            //            UserChosenId = idUserChosen,
            //            TypeVoteId = TypeVoteId,
            //            DateOfVote = DateTime.Today.ToString("MM-dd-yyyy"),
            //            Badge = badge,
            //            BadgeId = badge.Id,
            //            BadgeObtained = false,
            //        };
            //        var evente = _eventRepository.GetAll().Where(e => e.Date.ToString("MM-dd-yyyy") == voteHistory.DateOfVote).FirstOrDefault();
            //        if (evente != null)
            //        {
            //            DayEvent ListEvent = new DayEvent()
            //            {
            //                Action = "new vote",
            //                Date = DateTime.Today,
            //                Description = userOwner.UserName + " Has voted To " + userChosen.UserName,
            //                UserId = UserId,
            //                EventId = evente.Id,

            //            };
            //            var result = _eventRepository.CreateDayEvent(ListEvent);
            //            if (result)
            //            {
            //                voteHistory.DayEvent = ListEvent;
            //            }
            //        }
            //        _context.VoteHistories.Add(voteHistory);



            //        var userbadge = _UserbadgeRepository.GetUserBadge(idUserChosen, badge.Id);
            //        if (userbadge != null && userbadge.UserProgression <= userbadge.Badge.BadgeCriteria && userbadge.State != "Done" && DateTime.Now <= userbadge.BadgeDeadline)
            //        {
            //            userbadge.UserProgression += 1;
            //            _UserbadgeRepository.UpdateUserbadge(userbadge);
            //        }

            //        _context.SaveChanges();
            //    }


            //}
        
