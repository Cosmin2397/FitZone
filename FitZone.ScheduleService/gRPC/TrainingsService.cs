using FitZone.ScheduleService.Data;
using FitZone.ScheduleService.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FitZone.ScheduleService.gRPC
{
    public class TrainingsService : TrainingsGrpc.TrainingsGrpcBase
    {
        private readonly AppDbContext _context;

        public TrainingsService(AppDbContext context)
        {
            _context = context;
        }

        public override async Task<TrainingsResponse> GetTrainingsNumber(TrainingsRequest request, ServerCallContext context)
        {
            DateTime.TryParse(request.EndDate, out DateTime endDate);
            DateTime.TryParse(request.StartDate, out DateTime startDate);
            try
            {
                var trainings = await _context.Trainings.Where(g => g.GymId == Guid.Parse(request.GymId) && g.StartDate <= endDate && g.StartDate >= startDate && g.TrainingStatus == FitZone.ScheduleService.Entities.Enums.Status.Created).ToListAsync();
                return new TrainingsResponse
                {
                    NumOfFitnessClasses = trainings.Where(c => c.Type == FitZone.ScheduleService.Entities.Enums.TrainingType.FitnessClass).Count().ToString(),
                    NumOfPTTrainings = trainings.Where(c => c.Type == FitZone.ScheduleService.Entities.Enums.TrainingType.PersonalTrainer).Count().ToString(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
    }
}
