using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class UserSystemsVm
    {
        public int UserId { get; set; }

        public int SelectedSystemesID { get; set; }
        public IEnumerable<SelectListItem> systemes { get; set; }

    }
}
