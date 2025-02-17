using FitZone.ScheduleService.DTOs;
using FitZone.ScheduleService.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FitZone.ScheduleService.Entities
{
    public class Training
    {
        public Guid Id { get; set; }

        public string TrainingName { get; set; }

        public string TrainingDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public Guid GymId { get; set; }

        public Guid TrainerId { get; set; }

        public int Slots { get; set; }

        public TrainingType Type {get;set;}

        public Status TrainingStatus { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public string Comentariu { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Guid LastUpdatedBy { get; set; }

        [NotMapped]

        public List<TrainingSchedule> ScheduledClients { get; set; }
    }
}
