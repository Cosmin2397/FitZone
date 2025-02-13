using FitZone.GymsManagement.Dtos;
using FitZone.GymsManagement.Entities;

namespace FitZone.GymsManagement.Services.Interfaces
{
    public interface IGymService
    {
        Task<GymDTO> AddGym(GymDTO gym);

        Task<bool> RemoveGym(Guid id);

        Task<GymDTO> UpdateGym(GymDTO gym, Guid id);

        Task<GymDTO> GetGymById(Guid id);

        Task<List<GymDTO>> GetGyms();
    }
}
