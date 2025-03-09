using FitZone.SubscriptionValidationService.DTOs;
using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Protos;
using FitZone.SubscriptionValidationService.Repositories;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionValidationService.Services
{
    public class SubscriptionValidationService : FitnessStats.FitnessStatsBase, ISubscriptionValidationService
    {
        private readonly SubscriptionGrpc.SubscriptionGrpcClient _grpcClient;
        private readonly IValidationsRepository _repository;

        public SubscriptionValidationService(SubscriptionGrpc.SubscriptionGrpcClient grpcClient, IValidationsRepository repository)
        {
            _grpcClient = grpcClient;
            _repository = repository;
        }

        public async Task<bool> ValidateSubscription(ClientsAccess access)
        {
            var request = new SubscriptionRequest { Id = access.ClientId.ToString() , Date = access.DataValidare.ToString()};
            var response = await _grpcClient.GetSubscriptionByIdAsync(request);

            return response != null &&
                   DateTime.Parse(response.StartingDate) <= access.DataValidare &&
                   DateTime.Parse(response.EndDate) >= access.DataValidare;
        }


        public override async Task<EntriesResponse> GetEntriesAndExits(EntriesRequest request, ServerCallContext context)
        {
            var stats = await _repository.GetEntriesAndExitsAsync(DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate), Guid.Parse(request.GymId));

            var response = new EntriesResponse();
            response.Stats.AddRange(stats.Select(stat => new EntryStat
            {
                TimePeriod = stat.TimePeriod,
                Entries = stat.Entries,
                Exits = stat.Exits
            }));

            return response;
        }

    }

}
