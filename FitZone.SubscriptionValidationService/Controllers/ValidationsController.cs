using FitZone.SubscriptionValidationService.Models;
using FitZone.SubscriptionValidationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.SubscriptionValidationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {
        private readonly IValidationsService _validationsService;

        public ValidationsController(IValidationsService validationsService)
        {
            _validationsService = validationsService;
        }

        [HttpGet("/clientacceses")]
        public async Task<ActionResult<List<ClientsAccess>>> GetClientsAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            var accesses = await _validationsService.GetClientsAccesses(gymId,startDate,endDate);

            if (accesses == null)
            {
                return NotFound();
            }

            return accesses;
        }

        [HttpGet("/employeesacceses")]
        public async Task<ActionResult<List<ClientsAccess>>> GetEmployeesAccesses(Guid gymId, DateTime startDate, DateTime endDate)
        {
            var accesses = await _validationsService.GetEmployeesAccesses(gymId, startDate, endDate);

            if (accesses == null)
            {
                return NotFound();
            }

            return accesses;
        }
    }
}
