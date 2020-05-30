using Microsoft.AspNetCore.Http;
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
        public IFormFile Icon { get; set; }
        public int BadgeCriteria { get; set; }

        public string Challenge { get; set; }

        public int SystemeID { get; set; }
        public IEnumerable<SelectListItem> systemes { get; set; }

        public UserBadge UserBadge { get; set; }
    }
}
