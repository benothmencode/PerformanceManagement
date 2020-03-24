using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class EventEntityDto
    {

        public int Id { get; set; }

        public List<DayEventEntityDto> DayEvents { get; set; }

        public DateTime Date { get; set; }


    }
}
