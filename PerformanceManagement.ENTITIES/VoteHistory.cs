using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class VoteHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserChosenId { get; set; }
        [ForeignKey("UserChosenId")]
        public virtual ENTITIES.User UserChosen { get; set; }


        public int? UserOwnerId { get; set; }
        [ForeignKey("UserOwnerId")]
        public virtual ENTITIES.User UserOwner { get; set; }


        public int VoteRightsId { get; set; }
        
        public string DateOfVote { get; set; }

        [ForeignKey("TypeVoteId")]
        public int TypeVoteId { get; set; }
        public TypeVote TypeVote { get; set; }


    }
}
