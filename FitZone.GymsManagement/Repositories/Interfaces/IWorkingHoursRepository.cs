using FitZone.GymsManagement.Entities;

namespace FitZone.GymsManagement.Repositories.Interfaces
{
    public interface IWorkingHoursRepository
    {
        Task<BusinessHours> AddWorkingHours(BusinessHours workingHours);

        Task<bool> RemoveWorkingHours(Guid id);

        Task<BusinessHours> UpdateWorkingHours(BusinessHours gym, Guid id);

        Task<BusinessHours> GetWorkingHoursByDay(DayOfWeek day);

        Task<List<BusinessHours>> GetGymWorkingHours(Guid gymId);
    }
}
