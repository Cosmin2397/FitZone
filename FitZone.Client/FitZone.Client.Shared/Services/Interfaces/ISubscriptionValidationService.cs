using FitZone.Client.Shared.DTOs.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Services.Interfaces
{
    public interface ISubscriptionValidationService
    {
        public Task<List<ClientAccessDto>> GetClientsAccesses(DateTime startDate, DateTime endDate);

        public Task<List<ClientAccessDto>> GetEmployeesAccesses(DateTime startDate, DateTime endDate);
    }
}
