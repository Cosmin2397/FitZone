using FitZone.StatisticsService.Protos;

namespace FitZone.StatisticsService.gRPC
{
    public interface IStatisticsClient
    {
        Task<EntriesResponse> GetStatsAsync(EntriesRequest request);

        Task<ValidationResponse> GetValidationsByPeriodAsync(ValidationRequest request);

        Task<StatisticsResponse> GetSubscriptionsByPeriodAsync(StatisticsRequest request);

        Task<TrainingsResponse> GetSchedulesByPeriodAsync(TrainingsRequest request);
    }
}
