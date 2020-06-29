using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public interface IVoteRepository
    {
        IEnumerable<VoteRights> GetUserVoteRights(int idUser);
        VoteRights GetVoteRights(int id);

        //void CreateVoteHistory(int idUserChosen, int TypeVoteId, int UserId);

        IEnumerable<VoteHistory> GetVoteHistory(int UserId);
        void AddOrUpdateVoteRights(int id, VoteRights voteRights);
        List<TypeVote> GetTypeVotes();

        void CreateTypeVote(TypeVote typeVote);




    }
}