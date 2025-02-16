using FitZone.ScheduleService.Entities.Enums;

namespace FitZone.ScheduleService.DTOs
{
    public class TrainingDTO
    {

        public Guid Id { get; set; }

        public string TrainingName { get; set; }

        public string TrainingDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public Guid GymId { get; set; }

        public Guid TrainerId { get; set; }

        public int Slots { get; set; }

        public string Type { get; set; }

        public string TrainingStatus { get; set; }

        public string DifficultyLevel { get; set; }

        public string Comentariu { get; set; }

        public List<TrainingScheduleDTO>  ScheduledClients { get; set; }
    }
}
