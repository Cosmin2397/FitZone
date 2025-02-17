using FitZone.ScheduleService.Entities.Enums;

namespace FitZone.ScheduleService.Entities
{
    public class TrainingSchedule
    {
        public Guid Id { get;set; }

        public Guid TrainingId { get;set; }

        public Guid ClientId { get; set; }

        public TrainingScheduleStatus ScheduleStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }
    }
}
