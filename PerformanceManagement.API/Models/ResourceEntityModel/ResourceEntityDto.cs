using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PerformanceManagement.ENTITIES;

namespace PerformanceManagement.API.Models.ResourceEntityModel
{
    public class ResourceEntityDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public ENTITIES.ServiceSystem system { get; set; }


        public List<Resource_URI> URIList { get; set; }

    }
}
