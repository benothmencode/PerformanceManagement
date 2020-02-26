using PerformanceManagement.DATA.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.SystemRepository
{
    class SystemRepository : ISystemRepository
    {
        private readonly PerformanceManagementDBContext _context;
        public SystemRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddSystem(ENTITIES.ServiceSystem system)
        {

            if (system == null)
                throw new ArgumentNullException(nameof(system));

            system.Id = Guid.NewGuid();
            _context.ServiceSystems.Add(system);

        }
    }
}
