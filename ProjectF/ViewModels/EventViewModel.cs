using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ViewModels
{
    public class EventViewModel
    {

        public IList<EventEntityDto> events { get; set; }
        public IList<DayEventEntityDto> dayevents { get; set; }


    }
}
