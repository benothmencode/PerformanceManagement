using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class BadgesForVotes
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile Icon { get; set; }
        [Required]
        public int BadgeCriteria { get; set; }


        public int TypeVoteId { get; set; }
        public IEnumerable<SelectListItem> TypeVote { get; set; }
        public Periodicity Periodicity { get; set; }
        public int ValueOfPeriodicity { get; set; }
    }
}
