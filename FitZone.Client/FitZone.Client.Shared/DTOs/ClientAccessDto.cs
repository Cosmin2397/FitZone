using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
{
    public class ClientAccessDto
    {
        public Guid Id { get; set; }

        public Guid GymId { get; set; }

        public Guid ClientId { get; set; }

        public string ClientName { get; set; }

        public Guid SubscriptionId { get; set; }

        public int ValidationType { get; set; }

        public DateTime DataValidare { get; set; }
    }
}
