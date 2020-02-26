using PerformanceManagement.DATA.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceManagement.DATA.Repositories.ResourceRepository
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly PerformanceManagementDBContext _context;
        public ResourceRepository(PerformanceManagementDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public void AddResource(Guid SystemId ,ENTITIES.Resource resource)
        {
            if (SystemId == Guid.Empty)
                throw new ArgumentNullException(nameof(SystemId));

            if(resource == null)
                throw new ArgumentNullException(nameof(SystemId));

            resource.Id = Guid.NewGuid();
            resource.SystemId = SystemId;
            _context.Resources.Add(resource);



        }
    }
}
