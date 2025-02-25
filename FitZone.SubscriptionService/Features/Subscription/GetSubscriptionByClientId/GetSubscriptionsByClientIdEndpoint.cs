using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionByClientId
{
    public class GetSubscriptionsByClientIdEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/clientSubs/{clientId}", async ([FromRoute] Guid clientId, IMediator mediator) =>
            {
                var query = new GetSubscriptionByClientIdQuery(clientId);
                var subscriptions = await mediator.Send(query);
                return Results.Ok(subscriptions);
            });
        }
    }
}
