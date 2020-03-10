using Microsoft.AspNetCore.Http;

namespace ProjectF.ViewModel
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public IFormFile Userprofile { get; set; }
    }
}
