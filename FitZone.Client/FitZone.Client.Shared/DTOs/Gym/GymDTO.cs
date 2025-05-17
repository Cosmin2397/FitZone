using FitZone.Client.Shared.DTOs.Auth;

namespace FitZone.Client.Shared.DTOs.Gym
{
    public class GymDTO
    {
        public Gym GymData { get; set; }

        public List<BusinessHours> WeekBusinessHours { get; set; }

        public User GymManager { get; set; } 
    }

    public class Gym
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public Guid ManagerId { get; set; }

        public GymStatus Status { get; set; }

    }

    public class BusinessHours
    {
        public Guid Id { get; set; }

        public Guid GymId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpeningHour { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public bool IsClosed { get; set; }

        public string Comment { get; set; }
    }

    public enum GymStatus
    {
        Operational,
        Suspended,
        Closed
    }
}
