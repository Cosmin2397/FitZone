namespace FitZone.SubscriptionService.Features.Payment.GetPayments
{
    public sealed record PaymentResponse(Guid id, Guid subscriptionId, string status, string type, decimal amount, DateTime paymentDate);
}
