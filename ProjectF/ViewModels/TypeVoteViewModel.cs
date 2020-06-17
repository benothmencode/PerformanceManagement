using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class TypeVoteViewModel
    {
        public int TypeVoteId { get; set; }
        public List<SelectListItem> TypeVoteSelectListItems { get; set; }
    }
}
