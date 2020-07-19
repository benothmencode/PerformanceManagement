using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class SystemUserViewModel
    {

        public SystemeUser systemeUser { get; set; }
        public int IdUsersystemselected { get; set; }
        public IEnumerable<SelectListItem> UserSystemesIds { get; set; }
    }
}
