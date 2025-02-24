using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionService.Features.Subscription.GetSubscriptionById
{
    public class GetSubscriptionByIdEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/subscription/{id}", async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var query = new GetSubscriptionByIdQuery(id);
                var subscription = await mediator.Send(query);
                return Results.Ok(subscription);
            });
        }
    }
}
