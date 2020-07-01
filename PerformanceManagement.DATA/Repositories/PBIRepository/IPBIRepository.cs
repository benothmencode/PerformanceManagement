using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.PBIRepository
{
    public interface IPBIRepository
    {

        public void CreatePBI();
        public void Updateprogression(UserBadge ub, int progression);
        public void firstpbi();
        public List<string> getUserforabadge(string b);
        public List<string> badges();
        public List<PBIEntity> getAll();
    }
}
