using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Repositories;

namespace FitZone.SubscriptionValidationService.Services
{
    public class ValidationsService: IValidationsService
    {
        private readonly IValidationsRepository _validationsRepository;

        public ValidationsService(IValidationsRepository validationsRepository)
        {
            _validationsRepository = validationsRepository;
        }

        public async Task<bool> AddAccess(ClientsAccess access)
        {
            return await _validationsRepository.AddAccess(access);
        }

        public async Task<List<ClientsAccess>> GetClientsAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            return await _validationsRepository.GetClientsAccesses(gymId, startDate, endDate);
        }

        public async Task<List<ClientsAccess>> GetEmployeesAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            return await _validationsRepository.GetEmployeesAccesses(gymId, startDate, endDate);
        }
    }
}
