using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class VoteRightsEntityDto
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int Quantity { get; set; }
    }
}
