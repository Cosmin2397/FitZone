using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeByGymId;
using FitZone.EmployeeManagement.Application.Extensions;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler(IApplicationDbContext dbContext)
   : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
    {
        public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            // get Employee id
            // return result

            var Employee = await dbContext.Employees
                .Where(g => g.Id == EmployeeId.Of(query.id))
                .OrderBy(o => o.FullName.FirstName)
                .FirstOrDefaultAsync(cancellationToken);

            if (Employee != null)
            {
                    var contracts = await dbContext.EmployeesContracts.Where(e => e.EmployeeId == Employee.Id).ToListAsync();
                    if (contracts != null && contracts.Count > 0)
                    {
                        Employee.SetCotracts(contracts);
                    }
            }
            return new GetEmployeeByIdResult(Employee.ToEmployeeDto());
        }
    }
}
