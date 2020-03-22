using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
  
    public class User
    {
        [Key]
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
        [Required]
        public string Job { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Skills { get; set; }

        public List<VoteRights> VoteRights { get; set; }
        public List<UserBadge> UserBadges { get; set; }

    

    //public byte[] PasswordSalt { get; set; }

    //public byte[] PasswordHash { get; set; }

    [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; } 

        public User()
        {
            Created = DateTime.Now;
        }

        public DateTime Modified { get; set; }
       
        




        
        
        

    }
}
