using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ExernalServices
{
    public interface ICommitsController
    {
        public Task CountCommitsUser(int idBadge, int iduser);
        public Task<int> ListProjectsUserMemberof(int userId, int idBadge);
    } 
}
