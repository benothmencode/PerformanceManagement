using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Models.badgeEntityModel
{
    public class BadgeEntityDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public byte Icon { get; set; }
        [Required]
        public string Description { get; set; }
        public int BadgesCriteria { get; set; }
    }
}
