using FitZone.SubscriptionService.Shared.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitZone.SubscriptionService.Shared.Domain.Entities
{
    public class Payment: Audit.Audit
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentType Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
