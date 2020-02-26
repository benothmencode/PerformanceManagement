using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    public class ParameterValues
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        [ForeignKey("ResourceParameterId")]
        public Guid ResourceParameterId { get; set; }
        public ResourceParameter resourceParameter { get; set; }
    }
}
