using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionService.Features.Subscription.GetGymSubscriptionsByPeriod
{
    public class GetGymSubscriptionsByPeriodEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/subscriptions/{gymId}/{startDate}/{endDate}", async ([FromRoute] Guid gymId, DateTime startDate, DateTime endDate, IMediator mediator) =>
            {
                var query = new GetGymSubscriptionsByPeriodQuery(gymId,startDate,endDate);
                var subscriptions = await mediator.Send(query);
                return Results.Ok(subscriptions);
            });
        }
    }
}
