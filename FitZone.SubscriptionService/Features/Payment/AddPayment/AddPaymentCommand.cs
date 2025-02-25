using MediatR;

namespace FitZone.SubscriptionService.Features.Payment.AddPayment
{
    public sealed record AddPaymentCommand(Guid subscriptionId, string status, string type, decimal amount, DateTime paymentDate) : IRequest<Guid>;
}
