using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Models.ResourceEntityModel
{
    public class ResourceEntityForCreation
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
