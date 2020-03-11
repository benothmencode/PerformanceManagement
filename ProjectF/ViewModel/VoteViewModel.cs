﻿using ProjectF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModel
{
    public class VoteViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
    }
}
