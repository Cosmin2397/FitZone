using FitZone.ScheduleService.Entities.Enums;

namespace FitZone.ScheduleService.DTOs
{
    public class TrainingScheduleDTO
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string ScheduleStatus { get; set; }
    }
}
