using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerformanceManagement.ENTITIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ModelsDTOS
{
    public class BadgeForCreationDto : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        public string JobId { get; set; }

        public List<SelectListItem> JobIds { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public IFormFile Icon { get; set; }

        [Required]
        public int BadgeCriteria { get; set; }


        public int SystemeID { get; set; }

        public IEnumerable<SelectListItem> systemes { get; set; }

        public UserBadge UserBadge { get; set; }

        [Required]
        public Periodicity Periodicity { get; set; }

        [Required]
        public int ValueOfPeriodicity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Periodicity == Periodicity.Monthly && ValueOfPeriodicity >= 4 && ValueOfPeriodicity < 1)
            {
                yield return new ValidationResult(
                    "Value of Periodicity must be between 1 and 3",
                    new[] { nameof(ValueOfPeriodicity) });
            }
            if (Periodicity == Periodicity.Weekly && ValueOfPeriodicity >= 12 && ValueOfPeriodicity < 1)
            {
                yield return new ValidationResult(
                    "Value of Periodicity must be between 1 and 11",
                    new[] { nameof(ValueOfPeriodicity) });
            }
        }
    }
}
