using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.SystemRepository
{
    public interface ISystemRepository
    {
        void AddSystem(ENTITIES.ServiceSystem system);
    }
}
