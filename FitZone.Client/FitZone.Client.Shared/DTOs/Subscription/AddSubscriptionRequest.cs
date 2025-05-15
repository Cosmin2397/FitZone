using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs.Subscription
{
    public class AddSubscriptionRequest
    {
        public Guid clientId { get; set; }
        public Guid gymId { get; set; }
        public string subscriptionType { get; set; }
        public string clientType { get; set; }
        public DateTime startingDate { get; set; }
        public AddPaymentRequest payment { get; set; }
    }

    public class AddPaymentRequest
    {
        public Guid subscriptionId { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public decimal amount { get; set; }
        public DateTime paymentDate { get; set; }
    }

    public enum SubscriptionType
    {
        Normal,
        PersonalTrainer
    }

    public enum PaymentType
    {
        Cash,
        Stripe
    }

    public enum ClientType
    {
        Client,
        Employee
    }
}
