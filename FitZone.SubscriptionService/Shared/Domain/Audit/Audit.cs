namespace FitZone.SubscriptionService.Shared.Domain.Audit
{
    public class Audit
    {
        public Guid AddedBy { get;set; }

        public DateTime AddedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}
