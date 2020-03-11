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
        public Vote vote { get; set; }

        public AwardViewModel()
        {
        }

        public AwardViewModel(User user, Vote vote)
        {
            this.user = user;
            this.vote = vote;
        }
    }
}
