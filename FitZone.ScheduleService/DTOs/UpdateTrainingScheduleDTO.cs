using FitZone.ScheduleService.Entities.Enums;

namespace FitZone.ScheduleService.DTOs
{
    public class UpdateTrainingScheduleDTO
    {

        public Guid TrainingId { get; set; }

        public Guid ClientId { get; set; }

        public string ScheduleStatus { get; set; }
    }
}
