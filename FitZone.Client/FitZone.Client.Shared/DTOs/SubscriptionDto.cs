
namespace FitZone.Client.Shared.DTOs
{
    public class SubscriptionDto
    {
        public DateTime ValidUntil { get; set; }
        public string SubscriptionType { get; set; } = String.Empty;
        public GymDetailDto GymDetails { get; set; } = new GymDetailDto();
    }

    public class GymDetailDto
    {
        public string GymName { get; set; } = String.Empty;
        public string GymAddress { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
        public List<string> WorkingHours { get; set; } = new List<string>();
    }
}
