using ProjectF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModel
{
    public class AwardViewModel
    {
        public User user { get; set; }
        public string voteTitle { get; set; }

        public AwardViewModel()
        {
        }

        public AwardViewModel(User user, string voteT)
        {
            this.user = user;
            this.voteTitle = voteT;
        }
    }
}
