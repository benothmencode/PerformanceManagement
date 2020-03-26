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
            return _context.VoteRights.Where(v => v.UserId == idUser).ToList(); 
        }

        public VoteRights GetVoteRights(int id)
        {
            return _context.VoteRights.FirstOrDefault(vr => vr.Id == id);
        }

        public void CreateVoteHistory(int idUserChosen, int idVote, int UserId)
        {
            var voteR = GetVoteRights(idVote);
            string TitleVoteChosen = voteR.Title;
            VoteHistory voteHistory = new VoteHistory()
            {
                UserOwnerId = UserId,
                UserChosenId = idUserChosen,
                VoteRightsId = idVote,
                VoteTitle = TitleVoteChosen
            };

            voteHistory.DateOfVote = DateTime.UtcNow.ToString("MM-dd-yyyy");
            _context.VoteHistories.Add(voteHistory);
            _context.SaveChanges();

        }

        public IEnumerable<VoteHistory> GetVoteHistory()
        {
           return _context.VoteHistories.Include(vh => vh.UserChosen).Include(vh => vh.UserOwner).ToList();
        }
        
    }
}
