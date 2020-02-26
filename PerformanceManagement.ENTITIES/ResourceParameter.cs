using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    [Table("ResourceParameter")]
    public class ResourceParameter
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsSpecific { get; set; }
        public string Description { get; set; }

        
        public List<ParameterValues> ExpectedValues  { get; set; }

        [ForeignKey("ResourceId")]
        public Guid ResourceURIId { get; set; }
        public Resource_URI ResourceURI { get; set; }

    }
}
