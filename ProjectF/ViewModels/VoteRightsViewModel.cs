using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class VoteRightsViewModel
    {
        public IList<UserEntityDto> UsersDtos { get; set; }
        public IList<VoteRightsEntityDto> VoteRightsDtos { get; set; }
    }
}
