using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class SystemeEntityDto
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public string UrlUserSystemAccount { get; set; }

        public IList<Badge> Badges { get; set; }
    }
}
