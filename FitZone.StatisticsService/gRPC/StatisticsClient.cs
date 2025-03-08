using FitZone.StatisticsService.Protos;
using Grpc.Net.Client;

namespace FitZone.StatisticsService.gRPC
{
    public class StatisticsClient: IStatisticsClient
    {
        private readonly FitnessStats.FitnessStatsClient _client;
        private readonly FitnessStatsPeriod.FitnessStatsPeriodClient _clientPeriod;
        private readonly StatisticsGrpc.StatisticsGrpcClient _subscriptions;
        private readonly TrainingsGrpc.TrainingsGrpcClient _trainings;

        public StatisticsClient(FitnessStats.FitnessStatsClient client, FitnessStatsPeriod.FitnessStatsPeriodClient clientPeriod, StatisticsGrpc.StatisticsGrpcClient subscriptions, TrainingsGrpc.TrainingsGrpcClient trainings)
        {
            _client = client;
            _clientPeriod = clientPeriod;
            _subscriptions = subscriptions;
            _trainings = trainings;
        }

        public async Task<EntriesResponse> GetStatsAsync(EntriesRequest request)
        {
            var response = await _client.GetEntriesAndExitsAsync(request);

            return response;
        }

        public async Task<ValidationResponse> GetValidationsByPeriodAsync(ValidationRequest request)
        {
            var response = await _clientPeriod.GetEntriesAndExitsByPeriodAsync(request);

            return response;
        }

        public async Task<StatisticsResponse> GetSubscriptionsByPeriodAsync(StatisticsRequest request)
        {
            var response = await _subscriptions.GetSubscriptionNumberAsync(request);

            return response;
        }

        public async Task<TrainingsResponse> GetSchedulesByPeriodAsync(TrainingsRequest request)
        {
            var response = await _trainings.GetTrainingsNumberAsync(request);

            return response;
        }
    }
}
