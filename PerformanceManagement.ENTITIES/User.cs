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
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Userimage { get; set; }

        public string Job { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public string Skills { get; set; }

        public byte[] PasswordSalt { get; set; }
      
        public byte[] PasswordHash { get; set; }
       
        public DateTime Created { get; set; }
     
        public DateTime Modified { get; set; }
       
        




        
        
        

    }
}
