using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    [Table("ResourceParameter")]
    public class Resource_Parameter
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsSpecific { get; set; }
        public string Description { get; set; }

        public List<string> ExpectedValues  { get; set; }

        [ForeignKey("ResourceId")]
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }

    }
}
