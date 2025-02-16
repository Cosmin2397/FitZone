using AutoMapper;
using FitZone.ScheduleService.Data;
using FitZone.ScheduleService.DTOs;
using FitZone.ScheduleService.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitZone.ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        public readonly AppDbContext context;
        public readonly IMapper mapper;

        public TrainingsController(AppDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<TrainingDTO>>> GetPeriodTrainings(DateTime startDate, DateTime endDate, Guid gymId, TrainingType type)
        {
            try
            {
                var trainings = await context.Trainings.Include(x => x.ScheduledClients).Where(z => z.StartDate >= startDate && z.FinishDate <= endDate && z.GymId == gymId && z.Type == type).ToListAsync();

                return mapper.Map<List<TrainingDTO>>(trainings);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea antrenamentelor {ex.Message}");
                throw;
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> GetTrainingById(Guid id)
        {
            try
            {
                var training = await context.Trainings.Include(x => x.ScheduledClients).FirstOrDefaultAsync(z => z.Id == id);

                return mapper.Map<TrainingDTO>(training);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea antrenamentului cu id {id}  - {ex.Message}");
                throw;
            }
        }



        [HttpGet("list/{clientId}")]
        public async Task<ActionResult<List<TrainingDTO>>> GetTrainingsByClientId(Guid clientId)
        {
            try
            {
                var clientTrainings = await context.Trainings.Include(x => x.ScheduledClients).Where(z => z.ScheduledClients.Any(m => m.ClientId == clientId) && z.TrainingStatus != Status.Canceled).OrderBy(x => x.StartDate).ToListAsync();

                return mapper.Map<List<TrainingDTO>>(clientTrainings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea antrenamentelor clientului cu id {clientId}  - {ex.Message}");
                throw;
            }
        }

        [HttpGet("trainer/{clientId}")]
        public async Task<ActionResult<List<TrainingDTO>>> GetTrainingsByTrainerId(Guid trainerId)
        {
            try
            {
                var trainerTrainings = await context.Trainings.Include(x => x.ScheduledClients).Where(z => z.TrainerId == trainerId).OrderBy(x => x.StartDate).ToListAsync();

                return mapper.Map<List<TrainingDTO>>(trainerTrainings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea antrenamentelor trainerului cu id {trainerId}  - {ex.Message}");
                throw;
            }
        }
    }
}
