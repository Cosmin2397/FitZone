using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Protos;

namespace FitZone.SubscriptionValidationService.Services
{
    public class SubscriptionValidationService : ISubscriptionValidationService
    {
        private readonly SubscriptionGrpc.SubscriptionGrpcClient _grpcClient;

        public SubscriptionValidationService(SubscriptionGrpc.SubscriptionGrpcClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<bool> ValidateSubscription(ClientsAccess access)
        {
            var request = new SubscriptionRequest { Id = access.ClientId.ToString() };
            var response = await _grpcClient.GetSubscriptionByIdAsync(request);

            return response != null &&
                   DateTime.Parse(response.StartingDate) <= access.DataValidare &&
                   DateTime.Parse(response.EndDate) >= access.DataValidare;
        }

    }

}
