using FitZone.SubscriptionService.Features.Subscription.AddSubscription;
using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.AddPayment
{

    public class AddPaymentEnpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapPost("/payments", async (AddPaymentRequest request, IMediator mediator) =>
            {
                var command = new AddPaymentCommand(request.subscriptionId, request.status, request.type, request.amount, request.paymentDate);
                var paymentId = await mediator.Send(command);
                return Results.Ok(paymentId);
            });
        }
    }
}
