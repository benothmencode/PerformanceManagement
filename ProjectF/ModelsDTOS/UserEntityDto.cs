using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ProjectF.ModelsDTOS

{
    public class UserEntityDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Userimage { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public string Skills { get; set; }
        public DateTime Created { get; set; }




    }
}
