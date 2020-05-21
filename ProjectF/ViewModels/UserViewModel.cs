using Microsoft.AspNetCore.Http;
using System;

namespace ProjectF.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Userimage { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public string Skills { get; set; }
        public DateTime Modified { get; set; }
    }
}
