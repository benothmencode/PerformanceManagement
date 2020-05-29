using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class BadgeForCreationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Icon { get; set; }
        public int BadgeCriteria { get; set; }

        public string Challenge { get; set; }

        public int SelectedSystemesID { get; set; }
        public IEnumerable<SelectListItem> systemes { get; set; }

        public List<UserBadge> UserBadges { get; set; }
        public String Created { get; set; }
    }
}
