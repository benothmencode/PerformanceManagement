using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerformanceManagement.ENTITIES
{
    public class DayEvent 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Action { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public User User { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }

        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public List<VoteHistory> VoteHistories { get; set; }


    }
}
