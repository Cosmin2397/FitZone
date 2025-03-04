using FitZone.SubscriptionValidationService.Models;

namespace FitZone.SubscriptionValidationService.Services
{
    public interface IValidationsService
    {
        Task<List<ClientsAccess>> GetClientsAccesses(Guid gymId, DateTime startDate, DateTime endDate);

        Task<List<ClientsAccess>> GetEmployeesAccesses(Guid gymId, DateTime startDate, DateTime endDate);

        Task<bool> AddAccess(ClientsAccess access);
    }
}
