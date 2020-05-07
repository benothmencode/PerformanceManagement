using Microsoft.AspNetCore.Identity;
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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Icon { get; set; }
        [Required]
        public int BadgeCriteria { get; set; }

        //public  periodicity { get; set; }

     

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime Created { get; set; }
        public Badge()
        {
            Created = DateTime.Now;
        }

        [ForeignKey("SystemeId")]
        public int SystemeId { get; set; }
        public Systeme Systeme { get; set; }
        public List<UserBadge> UserBadges { get; set; }

        //public bool is_user { get; set; }(parsyst wala par user)

    }
}
