using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly PerformanceManagementDBContext _context;
        private readonly IUserBadgeRepository _UserbadgeRepository;
        private readonly IEventRepository _eventRepository;

        public VoteRepository(PerformanceManagementDBContext context, IUserBadgeRepository userBadgeRepository, IEventRepository eventRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _UserbadgeRepository = userBadgeRepository;
            _eventRepository = eventRepository;
        }

        public IEnumerable<VoteRights> GetUserVoteRights(int idUser)
        {
            return _context.VoteRights.Where(v => v.UserId == idUser).Include(v => v.TypeVote).ToList();
        }

        public VoteRights GetVoteRights(int id)
        {
            return _context.VoteRights.Include(v => v.TypeVote).FirstOrDefault(vr => vr.Id == id);
        }
        public VoteRights GetUserVoteRightsperType(int idUser, int TypeVoteId)
        {
            return _context.VoteRights.Where(v => v.UserId == idUser).Where(v => v.TypeVoteId == TypeVoteId).Include(v => v.TypeVote).FirstOrDefault();
        }

        public void CreateVoteHistory(int idUserChosen, int TypeVoteId, int UserId)
        {

            var badge = _context.Badges.Where(b => b.TypeVoteId == TypeVoteId).FirstOrDefault();
            var userOwner = _context.Users.FirstOrDefault(u => u.Id == UserId);
            var userChosen = _context.Users.FirstOrDefault(u => u.Id == idUserChosen);

            VoteHistory voteHistory = new VoteHistory()
            {
                UserOwnerId = UserId,
                UserChosenId = idUserChosen,
                TypeVoteId = TypeVoteId,
                DateOfVote = DateTime.Today.ToString("MM-dd-yyyy"),
                Badge = badge,
                BadgeId = badge.Id,
                BadgeObtained = false,
            };
            var evente = _eventRepository.GetAll().Where(e => e.Date.ToString("MM-dd-yyyy") == voteHistory.DateOfVote).FirstOrDefault();
            if (evente != null)
            {
                DayEvent ListEvent = new DayEvent()
                {
                    Action = "new vote",
                    Date = DateTime.Today,
                    Description = userOwner.UserName + " Has voted To " + userChosen.UserName,
                    UserId = UserId,
                    EventId = evente.Id,
                   
                };
                var result = _eventRepository.CreateDayEvent(ListEvent);
                if (result)
                {
                    voteHistory.DayEvent = ListEvent;
                }
            }
            _context.VoteHistories.Add(voteHistory);



            var userbadge = _UserbadgeRepository.GetUserBadge(idUserChosen, badge.Id);
            if (userbadge != null && userbadge.UserProgression <= userbadge.Badge.BadgeCriteria && userbadge.State != "Done" && DateTime.Now <= userbadge.BadgeDeadline)
            {
                userbadge.UserProgression += 1;
                _UserbadgeRepository.UpdateUserbadge(userbadge);
            }

            _context.SaveChanges();
        }

        public IEnumerable<VoteHistory> GetVoteHistory(int UserId)
        {
            return _context.VoteHistories.Where(vh => vh.UserOwnerId == UserId).Include(vh => vh.UserChosen).Include(vh => vh.UserOwner).ToList();
        }

        public void AddOrUpdateVoteRights(int id, VoteRights voteRights)
        {
            if (id == 0 && voteRights != null)
            {

                _context.Add(voteRights);
                _context.SaveChanges();
            }
            else
            {
                _context.VoteRights.Update(voteRights);
                _context.SaveChanges();
            }
        }

        public List<TypeVote> GetTypeVotes()
        {
            return _context.TypeVotes.Include(vt => vt.Badges).ToList();
        }



        void IVoteRepository.CreateTypeVote(TypeVote typeVote)
        {
            if (_context.TypeVotes.Any(x => x.Libellé == typeVote.Libellé))
                throw new Exception("Votes \"" + typeVote.Libellé + "\" exists already ");
            _context.Add(typeVote);
            _context.SaveChanges();
        }
    }
}