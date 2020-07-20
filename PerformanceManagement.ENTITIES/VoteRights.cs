using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class VoteRights
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("TypeVoteId")]
        public int TypeVoteId { get; set; }
        public TypeVote TypeVote { get; set; }
        public DateTime Update { get; set; }
        public bool BadgeDisabled { get; set; }




    }
}
