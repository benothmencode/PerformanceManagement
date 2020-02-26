using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class System
    {

        public Guid Id { get; set; }
        public string SystemName { get; set; }

        public string BasePath { get; set; }

        public List<Resource> ResourceList { get; set; }
    }
}
