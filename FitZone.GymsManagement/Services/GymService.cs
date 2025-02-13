using FitZone.GymsManagement.Dtos;
using FitZone.GymsManagement.Repositories.Interfaces;
using FitZone.GymsManagement.Services.Interfaces;

namespace FitZone.GymsManagement.Services
{
    public class GymService: IGymService
    {
        private readonly IGymRepository gymRepository;
        private readonly IWorkingHoursRepository workingHoursRepository;

        public GymService(IWorkingHoursRepository workingHoursRepository, IGymRepository gymRepository)
        {
            this.workingHoursRepository = workingHoursRepository;
            this.gymRepository = gymRepository;
        }

        public async Task<GymDTO> AddGym(GymDTO gym)
        {
            GymDTO addedGym = new GymDTO();
            if (gym != null)
            {
                if(gym.GymData != null)
                {
                    addedGym.GymData = await gymRepository.AddGym(gym.GymData);
                }
                if(gym.WeekBusinessHours != null)
                {
                    foreach (var workingHour in gym.WeekBusinessHours)
                    {
                        workingHour.GymId = addedGym.GymData.Id;
                        addedGym.WeekBusinessHours.Add(await workingHoursRepository.AddWorkingHours(workingHour));
                    }
                }
            }

            return addedGym;
        }

        public async Task<bool> RemoveGym(Guid id)
        {
            if(id != Guid.Empty )
            {
                return await gymRepository.RemoveGym(id) && await workingHoursRepository.RemoveWorkingHours(id);
            }

            return false;
        }

        public async Task<GymDTO> GetGymById(Guid id)
        {
            GymDTO gym = new GymDTO();
            if(id != Guid.Empty)
            {
                gym.GymData = await gymRepository.GetGymById(id);
                gym.WeekBusinessHours = await workingHoursRepository.GetGymWorkingHours(id);
            }

            return gym;
        }

        public async Task<List<GymDTO>> GetGyms()
        {
            List<GymDTO> gyms = new List<GymDTO>();
            var gymData = await gymRepository.GetGyms();
            foreach (var gym in gymData)
            {
                gyms.Add(new GymDTO
                {
                    GymData = gym,
                    WeekBusinessHours = await workingHoursRepository.GetGymWorkingHours(gym.Id)
                });
            }

            return gyms;
        }

        public async Task<GymDTO> UpdateGym(GymDTO gym, Guid id)
        {
            var updatedGym = new GymDTO();
            if (gym != null && id != Guid.Empty)
            {
                updatedGym.GymData = await gymRepository.UpdateGym(gym.GymData, id);
                if(updatedGym != null)
                {
                    foreach (var workingHour in gym.WeekBusinessHours)
                    {
                        updatedGym.WeekBusinessHours.Add(await workingHoursRepository.UpdateWorkingHours(workingHour, workingHour.Id));
                    }
                }
            }

            return updatedGym;
        }
    }
}
