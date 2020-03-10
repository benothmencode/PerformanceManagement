using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModel
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public IFormFile Userprofile { get; set; }
    }
}
