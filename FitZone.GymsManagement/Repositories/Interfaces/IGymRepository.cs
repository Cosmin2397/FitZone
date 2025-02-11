using FitZone.GymsManagement.Entities;

namespace FitZone.GymsManagement.Repositories.Interfaces
{
    public interface IGymRepository
    {
        Task<Gym> AddGym(Gym gym);

        Task<Gym> RemoveGym(Guid id);

        Task<Gym> UpdateGym(Gym gym, Guid id);

        Task<Gym> GetGymById(Guid id);

        Task<List<Gym>> GetGyms();
    }
}
