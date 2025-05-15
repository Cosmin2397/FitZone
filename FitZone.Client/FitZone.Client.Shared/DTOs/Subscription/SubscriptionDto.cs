namespace FitZone.Client.Shared.DTOs.Subscription
{
    public class SubscriptionDto
    {
        public DateTime ValidUntil { get; set; }
        public string SubscriptionType { get; set; } = string.Empty;
        public GymDetailDto GymDetails { get; set; } = new GymDetailDto();
    }

    public class GymDetailDto
    {
        public Guid GymId { get; set; }
        public string GymName { get; set; } = string.Empty;
        public string GymAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> WorkingHours { get; set; } = new List<string>();
    }
}
