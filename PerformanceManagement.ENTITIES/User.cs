using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PerformanceManagement.ENTITIES
{
  
    public class User : IdentityUser<int>
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Userimage { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Skills { get; set; }
        public List<VoteRights> VoteRights { get; set; }
        public List<UserBadge> UserBadges { get; set; }
        public List<SystemeUser> SystemeUsers { get; set; }

        public List<DayEvent> dayEvents { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public User()
        {
            this.Created = DateTime.UtcNow.Date;
            this.Modified = DateTime.UtcNow.Date;
        }
      
       
        




        
        
        

    }
}
