using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories
{
    public class VoteRightsRepository : IVoteRightsRepository
    {
        private readonly PerformanceManagementDBContext _context;
        public VoteRightsRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<VoteRights> GetUserVoteRights(int idUser)
        {
            return _context.VoteRights.Where(v => v.UserId == idUser).ToList(); 
        }
    }
}
