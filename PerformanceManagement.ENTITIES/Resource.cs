using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PerformanceManagement.ENTITIES
{
    [Table("Resource")]
    public class Resource
    {
        [Key]
        public Guid Id { get; set; }

        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }


        
        [ForeignKey("SystemId")]
        public Guid SystemId { get; set; }

        public ServiceSystem System { get; set; }


        public List<Resource_URI> URIList { get; set; }

        
    }
}
