using ProjectF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModel
{
    public class VoteViewModel
    {
        
        public IList<User> Users { get; set; }
        public IList<Vote> Votes { get; set; }
        public VoteViewModel(IList<User> users, IList<Vote> votes)
        {
            Users = users;
            Votes = votes;
        }

        public VoteViewModel()
        {
        }
    }
}
