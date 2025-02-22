using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeByGymId;
using FitZone.EmployeeManagement.Application.Extensions;
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
                           .Include(o => o.EmployeeContracts)
                           .OrderBy(o => o.FullName.FirstName)
                           .Where(g => g.Id.Value == query.id)
                           .FirstOrDefaultAsync(cancellationToken);

            return new GetEmployeeByIdResult(Employee.ToEmployeeDto());
        }
    }
}
