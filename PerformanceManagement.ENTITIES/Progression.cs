using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class Progression
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Userprogression { get; set; }
        public DateTime DateUserprog { get; set; }
        public string  UserName { get; set; }
        [ForeignKey("UserbadgeId")]
        public int UserBadgeId { get; set; }
        public UserBadge UserBadge { get; set; }
    }
}
