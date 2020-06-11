using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class TypeVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Libellé { get; set; }
        public IList<VoteRights> JetonVotes { get; set; }
        public IList<VoteHistory> VoteHistories { get; set; }
        public IList<Badge> Badges { get; set; }




    }
}
