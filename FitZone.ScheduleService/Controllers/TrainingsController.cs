using AutoMapper;
using FitZone.ScheduleService.Data;
using FitZone.ScheduleService.DTOs;
using FitZone.ScheduleService.Entities;
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

                if (training == null) return NotFound();
                return mapper.Map<TrainingDTO>(training);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea antrenamentului cu id {id}  - {ex.Message}");
                throw;
            }
        }

        [HttpGet("schedule/{id}")]
        public async Task<ActionResult<TrainingScheduleDTO>> GetScheduleById(Guid id)
        {
            try
            {
                var schedule = await context.TrainingSchedules.FirstOrDefaultAsync(z => z.Id == id);

                if (schedule == null) return NotFound();
                return mapper.Map<TrainingScheduleDTO>(schedule);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la aducerea programarii cu id {id}  - {ex.Message}");
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

        [HttpPost]
        public async Task<ActionResult<TrainingDTO>> AddTraining(CreateTrainingDTO trainingDto)
        {
            try
            {
                var training = mapper.Map<Training>(trainingDto);
                training.CreatedAt = DateTime.Now;
                training.CreatedBy = new Guid();
                training.LastUpdatedAt = DateTime.Now;
                training.LastUpdatedBy = new Guid();

                context.Trainings.Add(training);

                var result = await context.SaveChangesAsync() > 0;

                if(!result)
                {
                    return BadRequest("Datele privind antrenamentul nu au putut fi salvate în baza de date!");
                }

                return CreatedAtAction(nameof(GetTrainingById), new { training.Id }, mapper.Map<TrainingDTO>(training));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la crearea antrenamentului - {ex.Message}");
                throw;
            }
        }


        [HttpPost("createSchedule")]
        public async Task<ActionResult<TrainingScheduleDTO>> AddSchedule(CreateTrainingScheduleDTO scheduleDTO)
        {
            try
            {
                var training = await context.Trainings.FirstOrDefaultAsync(i => i.Id == scheduleDTO.TrainingId);
                training.ScheduledClients = await context.TrainingSchedules.Where(i => i.TrainingId == scheduleDTO.TrainingId).ToListAsync();
                if(training != null && training.TrainingStatus != Status.Created)
                {
                    return BadRequest("Antrenamentul pentru care s-a incercat programarea fie nu exista fie este anulat!");
                }

                if(training.ScheduledClients == null || training.ScheduledClients.Count < training.Slots)
                {
                    var schedule = mapper.Map<TrainingSchedule>(scheduleDTO);
                    schedule.CreatedAt = DateTime.Now;
                    schedule.CreatedBy = new Guid();
                    schedule.LastUpdatedAt = DateTime.Now;
                    schedule.LastUpdatedBy = new Guid();

                    context.TrainingSchedules.Add(schedule);

                    var result = await context.SaveChangesAsync() > 0;
                    if (!result)
                    {
                        return BadRequest($"Programare pentru antrenamentul cu id {scheduleDTO.TrainingId} nu a putut fi salvata in baza de date!");
                    }

                    return CreatedAtAction(nameof(GetScheduleById), new { schedule.Id }, mapper.Map<TrainingScheduleDTO>(schedule));
                }
                else
                {
                    return BadRequest($"Locurile pentru antrenamentul cu id-ul {scheduleDTO.TrainingId} sunt deja ocupate!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la crearea programarii - {ex.Message}");
                throw;
            }
        }

        [HttpPut("{trainingId}")]
        public async Task<ActionResult<TrainingDTO>> UpdateTraining(UpdateTrainingDTO trainingDto, Guid trainingId)
        {
            try
            {
                var training = await context.Trainings.FirstOrDefaultAsync(x => x.Id == trainingId);

                if(training == null)
                {
                    return BadRequest("Antrenamentul nu exista!");
                }

                training.TrainingName = trainingDto.TrainingName;
                training.TrainingDescription = trainingDto.TrainingDescription;
                training.StartDate = trainingDto.StartDate;
                training.FinishDate = trainingDto.FinishDate;
                training.TrainerId = trainingDto.TrainerId;
                training.Slots = trainingDto.Slots;
                training.TrainingStatus = Enum.Parse<Status>(trainingDto.TrainingStatus);
                training.DifficultyLevel = Enum.Parse<DifficultyLevel>(trainingDto.DifficultyLevel);
                training.Comentariu = trainingDto.Comentariu;
                training.LastUpdatedAt = DateTime.Now;
                training.LastUpdatedBy = new Guid();
                if(training.TrainingStatus == Status.Canceled)
                {
                    var schedules = await context.TrainingSchedules.Where(z => z.TrainingId == trainingId).ToListAsync();
                    if(schedules != null && schedules.Count > 0)
                    {
                        foreach(var schedule in schedules)
                        {
                            schedule.ScheduleStatus = TrainingScheduleStatus.TrainingCanceled;
                            var updateSchedule = mapper.Map<UpdateTrainingScheduleDTO>(schedule);
                            await UpdateSchedule(updateSchedule, schedule.Id);
                        }
                    }
                }
                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return BadRequest("Datele privind antrenamentul nu au putut fi modificate în baza de date!");
                }

                return CreatedAtAction(nameof(GetTrainingById), new { trainingId }, mapper.Map<TrainingDTO>(training));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea antrenamentului - {ex.Message}");
                throw;
            }
        }

        [HttpPut("updateSchedule/{scheduleId}")]
        public async Task<ActionResult<TrainingScheduleDTO>> UpdateSchedule(UpdateTrainingScheduleDTO scheduleDto, Guid scheduleId)
        {
            try
            {
                var schedule = await context.TrainingSchedules.FirstOrDefaultAsync(x => x.Id == scheduleId);

                if (schedule == null)
                {
                    return BadRequest("Programarea nu exista!");
                }

                schedule.ScheduleStatus = Enum.Parse<TrainingScheduleStatus>(scheduleDto.ScheduleStatus);
                schedule.LastUpdatedAt = DateTime.Now;
                schedule.LastUpdatedBy = new Guid();

                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return BadRequest("Datele privind programarea nu au putut fi modificate în baza de date!");
                }

                return CreatedAtAction(nameof(GetScheduleById), new { scheduleId }, mapper.Map<TrainingScheduleDTO>(schedule));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea programarii - {ex.Message}");
                throw;
            }
        }



        [HttpDelete("{trainingId}")]
        public async Task<ActionResult<bool>> DeleteTraining(Guid trainingId)
        {
            try
            {
                var training = await context.Trainings.FirstOrDefaultAsync(x => x.Id == trainingId);

                if (training == null)
                {
                    return BadRequest("Antrenamentul nu exista!");
                }

                context.Remove(training);

                var schedules = await context.TrainingSchedules.Where(z => z.TrainingId == trainingId).ToListAsync();
                if (schedules != null && schedules.Count > 0)
                {
                    foreach (var schedule in schedules)
                    {
                        schedule.ScheduleStatus = TrainingScheduleStatus.TrainingCanceled;
                        var updateSchedule = mapper.Map<UpdateTrainingScheduleDTO>(schedule);
                        await UpdateSchedule(updateSchedule, schedule.Id);
                    }
                }
                var result = await context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return BadRequest("Antrenamentul nu a putut fi sters din baza de date!");
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la stergerea antrenamentului - {ex.Message}");
                throw;
            }
        }
    }
}
