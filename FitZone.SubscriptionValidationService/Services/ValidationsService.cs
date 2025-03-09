using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Protos;
using FitZone.SubscriptionValidationService.Repositories;
using Grpc.Core;

namespace FitZone.SubscriptionValidationService.Services
{
    public class ValidationsService: FitnessStatsPeriod.FitnessStatsPeriodBase, IValidationsService
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

        public override async Task<ValidationResponse> GetEntriesAndExitsByPeriod(ValidationRequest request, ServerCallContext context)
        {
            var stats = await _validationsRepository.GetEntriesAndExitsByPeriodAsync(DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate), Guid.Parse(request.GymId), request.Role);

            var response = new ValidationResponse();
            response.NumOfEntries = stats.Entries.ToString();
            response.NumOfExits = stats.Exits.ToString();

            return response;
        }
    }
}
