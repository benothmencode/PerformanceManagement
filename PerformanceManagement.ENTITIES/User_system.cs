using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
   public class User_system
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string System_Name { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
        public User User { get; set; }

    }
}
