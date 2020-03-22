using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class VoteHistoryViewModel
    {
        public UserEntityDto user { get; set; }
        public string voteTitle { get; set; }

        public VoteHistoryViewModel()
        {
        }

        public VoteHistoryViewModel(UserEntityDto user, string voteT)
        {
            this.user = user;
            this.voteTitle = voteT;
        }
    }
}
