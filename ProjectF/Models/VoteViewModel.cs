using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class VoteViewModel
    {
        
        public IList<UserEntityDto> Users { get; set; }
        public IList<Vote> Votes { get; set; }
        public VoteViewModel(IList<UserEntityDto> users, IList<Vote> votes)
        {
            Users = users;
            Votes = votes;
        }

        public VoteViewModel()
        {
        }
    }
}
