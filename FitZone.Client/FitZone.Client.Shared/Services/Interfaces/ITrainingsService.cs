using FitZone.Client.Shared.DTOs.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface ITrainingsService
    {
        public Task<List<TrainingDto>> GetPeriodTrainings(DateTime startDate, DateTime endDate, string type);

        public Task<TrainingDto> GetTrainingById(Guid id);

        public Task<TrainingScheduleDto> GetScheduleById(Guid id);

        public Task<List<TrainingDto>> GetTrainingsByClientId(Guid clientId);

        public Task<List<TrainingDto>> GetTrainingsByTrainerId(Guid trainerId);
    }
}
