using FitZone.SubscriptionService.Protos;
using FitZone.SubscriptionService.Shared.Data;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.gRPC
{
    public class StatisticsService: StatisticsGrpc.StatisticsGrpcBase
    {
            private readonly AppDbContext _context;

            public StatisticsService(AppDbContext context)
            {
                _context = context;
            }

        public override async Task<StatisticsResponse> GetSubscriptionNumber(StatisticsRequest request, ServerCallContext context)
        {
            DateTime.TryParse(request.EndDate, out DateTime endDate);
            DateTime.TryParse(request.StartDate, out DateTime startDate);
            try
            {
                var subscriptions = await _context.Subscriptions.Where(g => g.GymId == Guid.Parse(request.GymId) && g.EndDate <= endDate && g.StartingDate >= startDate && g.Status == Shared.Domain.Enums.Status.Active).ToListAsync();
                return new StatisticsResponse
                {
                    NumOfNormalSubscriptions = subscriptions.Where(c => c.Type == Shared.Domain.Enums.SubscriptionType.Normal).Count().ToString(),
                    NumOFPTSubscriptions = subscriptions.Where(c => c.Type == Shared.Domain.Enums.SubscriptionType.PersonalTrainer).Count().ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
    }
    }
