using FitZone.StatisticsService.gRPC;
using FitZone.StatisticsService.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.StatisticsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsClient _statisticsClient;

        public StatisticsController(IStatisticsClient statisticsClient)
        {
            _statisticsClient = statisticsClient;
        }

        [HttpGet("hourly")]
        public async Task<IActionResult> GetHourlyValidationsStats(DateTime startDate, DateTime endDate, Guid gymId)
        {
            var request = new EntriesRequest
            {
                StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss"),
                GymId = gymId.ToString()
            };

            var response = await _statisticsClient.GetStatsAsync(request);

            return Ok(response.Stats);
        }

        [HttpGet("period")]
        public async Task<IActionResult> GetPeriodValidationStats(DateTime startDate, DateTime endDate, Guid gymId, string role)
        {
            var request = new ValidationRequest
            {
                StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss"),
                GymId = gymId.ToString(),
                Role = role
            };

            var response = await _statisticsClient.GetValidationsByPeriodAsync(request);

            return Ok(response);
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetPeriodSubscriptions(DateTime startDate, DateTime endDate, Guid gymId)
        {
            var request = new StatisticsRequest
            {
                StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss"),
                GymId = gymId.ToString(),
            };

            var response = await _statisticsClient.GetSubscriptionsByPeriodAsync(request);

            return Ok(response);
        }

        [HttpGet("trainings")]
        public async Task<IActionResult> GetPeriodTrainings(DateTime startDate, DateTime endDate, Guid gymId)
        {
            var request = new TrainingsRequest
            {
                StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss"),
                GymId = gymId.ToString(),
            };

            var response = await _statisticsClient.GetSchedulesByPeriodAsync(request);

            return Ok(response);
        }
    }
}
