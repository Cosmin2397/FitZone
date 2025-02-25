using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.GetPayments
{


    public class GetPaymentsEnpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/payments", async (IMediator mediator) =>
            {
                var query = new GetPaymentsQuery();
                var payments = await mediator.Send(query);
                return Results.Ok(payments);
            });
        }
    }
}
