using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public interface IVoteRightsRepository
    {
        IEnumerable<VoteRights> GetUserVoteRights(int idUser);
        VoteRights GetVoteRights(int id);

    }
}
