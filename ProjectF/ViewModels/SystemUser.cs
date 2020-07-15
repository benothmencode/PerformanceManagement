using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class SystemUser
    {
      

        public int userId { get; set; }
      public  IList<SystemeUser> systemeUsers { get; set; }
      public IList<SelectListItem> Systemes { get; set; }

    }
}
