namespace FitZone.GymsManagement.Entities
{
    public class BusinessHours
    {
        public Guid Id { get; set; }

        public Guid GymId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpeningHour { get; set; }

        public TimeSpan ClosingTime { get; set;}

        public bool IsClosed { get; set; }

        public string Comment { get; set; }
    }
}
