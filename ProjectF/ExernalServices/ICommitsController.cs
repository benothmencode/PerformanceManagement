using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ExernalServices
{
    public interface ICommitsController
    {
        public Task<int> nombreCommits(int userId, int idBadge , DateTime? update);
    } 
}
