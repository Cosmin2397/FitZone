using FitZone.SubscriptionService.Features.Subscription.AddSubscription;
using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.UpdateSubscription
{
    public class UpdateSubscriptionEnpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPut("/subscriptions", async (UpdateSubscriptionRequest request, IMediator mediator) =>
            {
                var command = new UpdateSubscriptionCommand(request.subId, request.status, request.subscriptionType, request.clientType, request.addedDays,request.personalTrainerID, request.payment);
                var isSucces = await mediator.Send(command);
                return Results.Ok(isSucces);
            });
        }
    }
}
