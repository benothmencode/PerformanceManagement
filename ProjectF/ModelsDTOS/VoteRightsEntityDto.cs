using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class VoteRightsEntityDto
    {
        public int Id { get; set; }
        public int TypeVoteId { get; set; }
        public TypeVote TypeVote { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
