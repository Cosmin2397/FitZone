namespace FitZone.ScheduleService.DTOs
{
    public class CreateTrainingScheduleDTO
    {
        public Guid TrainingId { get; set; }

        public Guid ClientId { get; set; }
    }
}
