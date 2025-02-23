using FitZone.SubscriptionService.Shared.Domain.Enums;
using FitZone.SubscriptionService.Shared.Domain.Audit;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitZone.SubscriptionService.Shared.Domain.Entities
{
    public class Subscription: Audit.Audit
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid GymId { get; set; }

        public SubscriptionType Type { get; set; }

        public ClientType ClientType {get;set;}

        public Status Status { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EndDate { get; set; }

        [NotMapped]
        public List<Payment> SubscriptionPayments { get; set; }


    }
}
