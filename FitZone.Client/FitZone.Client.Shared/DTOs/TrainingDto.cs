using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
{
    public class TrainingDto
    {
        public Guid Id { get; set; }

        public string TrainingName { get; set; }

        public string TrainingDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public Guid GymId { get; set; }

        public Guid TrainerId { get; set; }

        public int Slots { get; set; }

        public int FreeSlots { get; set; }

        public string Type { get; set; }

        public string TrainingStatus { get; set; }

        public string DifficultyLevel { get; set; }

        public string Comentariu { get; set; }

        public List<TrainingScheduleDto> ScheduledClients { get; set; }
    }
}
