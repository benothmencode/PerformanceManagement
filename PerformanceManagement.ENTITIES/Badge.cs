using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class Badge
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public byte Icon { get; set; }
        [Required]
        public string Description { get; set; }
        public int BadgesCriteria { get; set; }
        public ICollection<UserBadge> UserBadges { get; set; }
    }
}
