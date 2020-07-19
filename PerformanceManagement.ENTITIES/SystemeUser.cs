using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class SystemeUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SystemeId { get; set; }
        public Systeme Systeme { get; set; }

        public bool SystemIsArchieved { get; set; }

        public string UrlUserSystemAccount { get; set; }
        public int? Identifier { get; set; }
    }
}
