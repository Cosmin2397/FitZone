using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using FitZone.SubscriptionService.Shared.Abstractions;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptions
{
    public class GetSubscriptionsEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/subscriptions", async (IMediator mediator) =>
            {
                var query = new GetSubscriptionQuery();
                var subscriptions = await mediator.Send(query);
                return Results.Ok(subscriptions);
            });
        }
    }
}