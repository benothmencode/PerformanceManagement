using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerformanceManagement.ENTITIES
{
    public enum Periodicity
    {
        Weekly,
        Monthly,
        Yearly
    }

    public class Badge 
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Icon { get; set; }
        [Required]
        public int BadgeCriteria { get; set; }


        public bool IsArchieved { get; set; }
        public bool SystemIsArchieved { get; set; }


        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime Created { get; set; }
        public Badge()
        {
            Created = DateTime.Now;
        }

        [ForeignKey("SystemeId")]
        public int? SystemeId { get; set; }
        public Systeme Systeme { get; set; }

        public string jobId { get; set; }
        public List<UserBadge> UserBadges { get; set; }
        public List<VoteHistory> voteHistories { get; set; }

        [ForeignKey("TypeVoteId")]
        public int? TypeVoteId { get; set; }
        public TypeVote TypeVote { get; set; }
        public DateTime LastCreation { get; set; }


        [Required]
        public Periodicity periodicity { get; set; }

        [Required]
        public int ValueOfPeriodicity { get; set; }

    }
}
