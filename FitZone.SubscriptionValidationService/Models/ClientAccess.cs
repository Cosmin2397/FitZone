using FitZone.SubscriptionValidationService.Models.Enums;

namespace FitZone.SubscriptionValidationService.Models
{
    public class ClientsAccess
    {
        public Guid Id { get; set; }

        public Guid GymId { get; set; }

        public Guid ClientId { get; set; }

        public Guid SubscriptionId { get;set; }

        public Role Role { get; set; }

        public ValidationType ValidationType { get; set; }

        public DateTime DataValidare { get; set; }
    }
}
