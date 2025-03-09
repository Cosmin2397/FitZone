using FitZone.SubscriptionValidationService.DTOs;
using FitZone.SubscriptionValidationService.Models;

namespace FitZone.SubscriptionValidationService.Repositories
{
    public interface IValidationsRepository
    {
       Task<List<ClientsAccess>> GetClientsAccesses(Guid gymId, DateTime startDate, DateTime endDate);

       Task<List<ClientsAccess>> GetEmployeesAccesses(Guid gymId, DateTime startDate, DateTime endDate);

        Task<bool> AddAccess(ClientsAccess access);

        Task<List<ValidationStatDto>> GetEntriesAndExitsAsync(DateTime startDate, DateTime endDate, Guid gymId);

        Task<ValidationByPeriodDto> GetEntriesAndExitsByPeriodAsync(DateTime startDate, DateTime endDate, Guid gymId, string role);
    }
}
