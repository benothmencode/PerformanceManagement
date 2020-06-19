using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{

   public  class Event
    {
        

        public int Id { get; set; }

        public List<DayEvent> DayEvent { get; set; }
        [Required]
        public DateTime Date { get; set; }
   
        public Event()
        {
            Date = DateTime.Today;
        }


    }
}
