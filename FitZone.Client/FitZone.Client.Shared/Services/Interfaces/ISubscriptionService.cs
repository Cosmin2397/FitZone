using FitZone.Client.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public Task<SubscriptionDto> GetSubscriptionByClientId(string id);
    }
}
