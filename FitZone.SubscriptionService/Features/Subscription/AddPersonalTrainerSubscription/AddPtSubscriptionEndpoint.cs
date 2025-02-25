using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Subscription.AddSubscription
{
    public class AddPtSubscription : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/addptsub", async (AddPtSubscriptionRequest request, IMediator mediator) =>
            {
                var command = new AddPtSubscriptionCommand(request.clientId,request.gymId,request.startingDate,request.ptId, request.payment);
                var subId = await mediator.Send(command);
                return Results.Ok(subId);
            });
        }
    }
}
