﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Userprofile { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public string Skills { get; set; }
        public List<Badge> Badges = new List<Badge>();
   


    }
}
