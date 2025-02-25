using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionService.Features.Payment.GetPaymentByID
{
    public class GetPaymentByIdEnpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/payment/{id}", async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var query = new GetPaymentByIdQuery(id);
                var payment = await mediator.Send(query);
                return Results.Ok(payment);
            });
        }
    }
}
