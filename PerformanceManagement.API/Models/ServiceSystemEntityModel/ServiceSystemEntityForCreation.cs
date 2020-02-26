using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.API.Models.SystemEntityModel
{
    public class ServiceSystemEntityForCreation
    {
        [Required]
        public string SystemName { get; set; }

        [Required]
        public string BasePath { get; set; }

    }
}
