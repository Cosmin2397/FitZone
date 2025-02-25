using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public class AddSubscription : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/subscriptions", async (AddSubscriptionRequest request, IMediator mediator) =>
            {
                var command = new AddSubscriptionCommand(request.clientId,request.gymId,request.subscriptionType,request.clientType,request.startingDate,request.payment);
                var subId = await mediator.Send(command);
                return Results.Ok(subId);
            });
        }
    }
}
