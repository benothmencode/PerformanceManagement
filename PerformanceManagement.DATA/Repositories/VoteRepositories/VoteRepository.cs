using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
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
        public VoteRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<VoteRights> GetUserVoteRights(int idUser)
        {
            return _context.VoteRights.Where(v => v.UserId == idUser).Include(v => v.TypeVote).ToList(); 
        }

        public VoteRights GetVoteRights(int id)
        {
            return _context.VoteRights.Include(v => v.TypeVote).FirstOrDefault(vr => vr.Id == id);
        }
        public VoteRights GetUserVoteRightsperType(int idUser , int TypeVoteId)
        {
            return _context.VoteRights.Where(v => v.UserId == idUser).Where(v => v.TypeVoteId == TypeVoteId).Include(v => v.TypeVote).FirstOrDefault();
        }

        public void CreateVoteHistory(int idUserChosen, int TypeVoteId, int UserId)
        {
            var voteR = GetUserVoteRightsperType(UserId, TypeVoteId);
            if(voteR.Quantity != 0)
            {
                voteR.Quantity -= 1;
            }
            AddOrUpdateVoteRights(voteR.Id , voteR);
            var badge = _context.Badges.Where(b => b.TypeVoteId == TypeVoteId).FirstOrDefault();
            VoteHistory voteHistory = new VoteHistory()
            {
                UserOwnerId = UserId,
                UserChosenId = idUserChosen,
                TypeVoteId = voteR.TypeVoteId, 
                DateOfVote = DateTime.Now.ToString("MM-dd-yyyy"),
                Badge = badge ,
                BadgeObtained = false ,
            };
           
            _context.VoteHistories.Add(voteHistory);
            _context.SaveChanges();

        }

        public IEnumerable<VoteHistory> GetVoteHistory(int UserId)
        {
           return _context.VoteHistories.Where(vh => vh.UserOwnerId == UserId).Include(vh => vh.UserChosen).Include(vh => vh.UserOwner).ToList();
        }
        
        public void AddOrUpdateVoteRights( int id ,VoteRights voteRights)
        {
            if(id == 0 && voteRights != null)
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
        public double NumberTokens(int idTypeVote)
        {
            
            var nbrWEEKS = new int();
            var typeVote = _context.TypeVotes.Find(idTypeVote);
            var badgeTypeVote = _context.Badges.Where(b => b.TypeVoteId == idTypeVote).FirstOrDefault();
            var critiria = badgeTypeVote.BadgeCriteria;
            var Periodicity = badgeTypeVote.periodicity;
            var Value = badgeTypeVote.ValueOfPeriodicity;
            if (Periodicity.Equals("Weekly"))
            {
                nbrWEEKS = Value;
            };
            if (Periodicity.Equals("Monthly") )
            {
                nbrWEEKS = Value * 4;
            };
            if (Periodicity.Equals("Yearly"))
            {
                nbrWEEKS = Value * 12 * 4;
            }
            var nbreUsers = _context.Users.Count();
            double res = (critiria * nbreUsers) / (nbrWEEKS * (nbreUsers - 1));
            var nbreToken = Math.Ceiling(res);
            return nbreToken;
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
