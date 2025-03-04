using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId;
using FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById;
using FitZone.SubscriptionService.Protos;
using Grpc.Core;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitZone.SubscriptionService.gRPC
{

    public class SubscriptionGrpcService : SubscriptionGrpc.SubscriptionGrpcBase
    {
        private readonly IMediator _mediator;

        public SubscriptionGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<SubscriptionResponse> GetSubscriptionById(SubscriptionRequest request, ServerCallContext context)
        {
            var query = new GetSubscriptionByClientIdQuery(Guid.Parse(request.Id));
            var subscriptions = await _mediator.Send(query);

            if (subscriptions == null)
            {
                return null;
            }

            var subscription = subscriptions.OrderByDescending(d => d.endDate).First();

            return new SubscriptionResponse
            {
                Id = subscription.id.ToString(),
                ClientId = subscription.clientId.ToString(),
                GymId = subscription.gymId.ToString(),
                SubscriptionType = subscription.subscriptionType,
                ClientType = subscription.clientType,
                Status = subscription.status,
                StartingDate = subscription.startingDate.ToString("O"),
                EndDate = subscription.endDate.ToString("O"),
            };
        }
    }

}
