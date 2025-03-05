using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById;
using FitZone.SubscriptionService.Protos;
using FitZone.SubscriptionService.Shared.Data;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitZone.SubscriptionService.gRPC
{

    public class SubscriptionGrpcService : SubscriptionGrpc.SubscriptionGrpcBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;

        public SubscriptionGrpcService(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public override async Task<SubscriptionResponse> GetSubscriptionById(SubscriptionRequest request, ServerCallContext context)
        {
            DateTime.TryParse(request.Date, out DateTime date);
            try
            {
                var subscription = await _context.Subscriptions.Where(g => g.GymId == Guid.Parse(request.Id) && g.EndDate >= date && g.StartingDate <= date && g.Status == Shared.Domain.Enums.Status.Active).FirstOrDefaultAsync();
                if(subscription != null)
                {
                    return new SubscriptionResponse
                    {
                        Id = subscription.Id.ToString(),
                        ClientId = subscription.ClientId.ToString(),
                        GymId = subscription.GymId.ToString(),
                        SubscriptionType = subscription.Type.ToString(),
                        ClientType = subscription.ClientType.ToString(),
                        Status = subscription.Status.ToString(),
                        StartingDate = subscription.StartingDate.ToString("O"),
                        EndDate = subscription.EndDate.ToString("O"),
                    };
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
    }

}
