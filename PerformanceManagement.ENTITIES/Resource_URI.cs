using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    [Table("ResourceURI")]
    public class Resource_URI
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int URI_Order { get; set; }

        [Required]
        public  string URI_value  { get; set; }

        
        public bool IsConfigrable { get; set; }

        public string ConfigValue { get; set; }

        public List<ResourceParameter> ParameterList { get; set; }

        [ForeignKey("ResourceId")]
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
