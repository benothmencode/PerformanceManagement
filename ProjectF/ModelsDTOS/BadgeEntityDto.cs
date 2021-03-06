﻿using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class BadgeEntityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Icon { get; set; }
        public int BadgeCriteria { get; set; }
        public TypeVote TypeVote { get; set; }
        public Periodicity periodicity { get; set; }

        
        public int ValueOfPeriodicity { get; set; }
        public string Challenge { get; set; }

        public SystemeEntityDto Systeme { get; set; }

        public List<UserBadge> UserBadges { get; set; }
        public String Created { get; set; }
    }
}
