using FitZone.Client.Shared.DTOs.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs.Auth
{
    public class User
    {
        public UserDto UserDto { get; set; }

        public SubscriptionDto SubscriptionDto { get; set; }
    }
}
