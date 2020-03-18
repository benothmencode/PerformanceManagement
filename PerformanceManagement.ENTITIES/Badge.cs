using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class Badge
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string System { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int BadgeCriteria { get; set; }
     
        public string Challenge { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartedAt { get; set; }
        public DateTime Created { get; set; }
        public Badge()
        {
            Created = DateTime.Now;
        }
  
        public List<UserBadge> UserBadges { get; set; }

    }
}
