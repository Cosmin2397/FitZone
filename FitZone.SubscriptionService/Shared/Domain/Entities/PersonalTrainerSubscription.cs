namespace FitZone.SubscriptionService.Shared.Domain.Entities
{
    public class PersonalTrainerSubscription
    {
        public Guid Id { get; set; }

        public Guid SubscriptionID { get; set; }

        public Guid PersonalTrainerID { get;set; }
    }
}
