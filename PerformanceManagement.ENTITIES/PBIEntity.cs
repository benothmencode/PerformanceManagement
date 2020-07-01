using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
   public  class PBIEntity
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("UserProgression")]

        public int UserProgression { get; set; }

        public int userbadgeId { get; set; }
        

        //[ForeignKey("Username")]

        public string Username { get; set; }
        //public User user { get; set; }

        //[ForeignKey("BadgeTitle")]

        public string BadgeTitle { get; set; }
        //public Badge badge { get; set; }


    }
}
