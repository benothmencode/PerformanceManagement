using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class UserProfileViewModel
    {
        public UserEntityDto user { get; set; }
        public IList<BadgeEntityDto> badges { get; set; }
    }
}
