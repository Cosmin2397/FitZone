using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
{
    public sealed record AddSubscriptionRequest(Guid clientId, Guid gymId, string subscriptionType, string clientType, DateTime startingDate, AddPaymentRequest payment);

    public sealed record AddPaymentRequest(Guid subscriptionId, string status, string type, decimal amount, DateTime paymentDate);
}
