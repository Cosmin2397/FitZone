using FitZone.Client.Shared.DTOs.Gym;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface IGymService
    {
        Task<List<GymDTO>> GetGyms();

        Task<GymDTO> GetGymById(Guid id);

        Task<GymDTO> UpdateGym(GymDTO gymUpdated, Guid id);

        Task<GymDTO> AddGym(GymDTO gym);

        Task<bool> DeleteGym(Guid id);
    }
}
