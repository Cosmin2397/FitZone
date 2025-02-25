using FitZone.SubscriptionService.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionService.Features.Payment.GetGymPayments
{
    public class GetGymPaymentsEnpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/gymPayments/{id}", async ([FromRoute] Guid id, IMediator mediator) =>
            {
                var query = new GetGymPaymentsQuery(id);
                var payments = await mediator.Send(query);
                return Results.Ok(payments);
            });
        }
    }
}
