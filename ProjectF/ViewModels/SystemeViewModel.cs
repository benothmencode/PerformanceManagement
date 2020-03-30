using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class SystemeViewModel
    {
        //public SystemeEntityDto Systeme { get; set; }
        public int SystemeId { get; set; }
        public List<SelectListItem> SystemeSelectListItems { get; set; }
    }
}
