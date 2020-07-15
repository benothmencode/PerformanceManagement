using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class UserSystemForNewSysteme
    {

        public int SystemeId { get; set; }
        public IList<PerformanceManagement.ENTITIES.SystemeUser> systemeUsers { get; set; }
        public IList<SelectListItem> Users { get; set; }
    }
}
