using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class UsersForAdmin
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<string> RolesHeld { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Userimage { get; set; }
        public string Created { get; set; }
        public List<UsersForAdmin> ListusersForAdmins { get; set; }

        public UsersForAdmin(User aus, List<string> userRoles)
        {
            UserId = aus.Id;
            UserName = aus.UserName;
            RolesHeld = userRoles; 
            Email = aus.Email;
            EmailConfirmed = aus.EmailConfirmed;
            LockoutEnabled = aus.LockoutEnabled;
            AccessFailedCount = aus.AccessFailedCount;
            Userimage = aus.Userimage;
            Created = aus.Created.ToString("dd/MM/yyyy");
        }
        public UsersForAdmin(List<UsersForAdmin> listusersForAdmins)
        {
            ListusersForAdmins = listusersForAdmins;
        }

    }
}
