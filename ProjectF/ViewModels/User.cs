using System.Collections.Generic;

namespace ProjectF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Userimage { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public string Skills { get; set; }

        //public IList<Badge> Badges { get; set; }
        public IList<Vote> Votes { get; set; }


    }
}
