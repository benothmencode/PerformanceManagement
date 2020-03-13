using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class AwardViewModel
    {
        public UserEntityDto user { get; set; }
        public string voteTitle { get; set; }

        public AwardViewModel()
        {
        }

        public AwardViewModel(UserEntityDto user, string voteT)
        {
            this.user = user;
            this.voteTitle = voteT;
        }
    }
}
