using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    [Table("User")]
    public class User
    {
        [Key]
       // [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        //[Required]
        public string Email { get; set; }
        //[Required]
        public string FirstName { get; set; }
       // [Required]
        public string LastName { get; set; }
        //[Required]
        public string Username { get; set; }
        
        public byte[] PasswordSalt { get; set; }
      
        public byte[] PasswordHash { get; set; }
       
        public DateTime Created { get; set; }
     
        public DateTime Modified { get; set; }
       
        [Required]
 
        public ICollection<UserBadge> UserBadges { get; set; }
        public ICollection<User_system> User_Systems { get; set; }

    }
}
