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

        public void CreateVoteHistory(int idUserChosen, int idVote, int UserId)
        {
            var voteR = GetVoteRights(idVote);
            string TitleVoteChosen = voteR.TypeVote.Libellé;
            VoteHistory voteHistory = new VoteHistory()
            {
                UserOwnerId = UserId,
                UserChosenId = idUserChosen,
                VoteRightsId = idVote,
                TypeVoteId = voteR.TypeVoteId, 
            };

            voteHistory.DateOfVote = DateTime.UtcNow.ToString("MM-dd-yyyy");
            _context.VoteHistories.Add(voteHistory);
            _context.SaveChanges();

        }

        public IEnumerable<VoteHistory> GetVoteHistory(int UserId)
        {
           return _context.VoteHistories.Where(vh => vh.UserOwnerId == UserId).Include(vh => vh.UserChosen).Include(vh => vh.UserOwner).ToList();
        }
        
    }
}
