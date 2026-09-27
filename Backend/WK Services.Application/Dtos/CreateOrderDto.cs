using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK_Services.Domain.Enums;

namespace WK_Services.Application.Dtos
{
    public class CreateOrderDto : IValidatableObject
    {
        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Range(1, 5, ErrorMessage = "Quantity must be between 1 and 5.")]
        public int Quantity { get; set; }

        public DateTime? RequestedDeliveryDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RequestedDeliveryDate.HasValue && RequestedDeliveryDate.Value.Date <= DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(
                    "Requested delivery date must be after today.",
                    new[] { nameof(RequestedDeliveryDate) });
            }
        }
    }
}