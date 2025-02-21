using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployees;
using FitZone.EmployeeManagement.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeByGymId
{
    public class GetEmployeesByGymIdHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetEmployeesByGymIdQuery, GetEmployeesByGymIdResult>
    {
        public async Task<GetEmployeesByGymIdResult> Handle(GetEmployeesByGymIdQuery query, CancellationToken cancellationToken)
        {
            // get Employees by gym id
            // return result

            var totalCount = await dbContext.Employees.LongCountAsync(cancellationToken);

            var Employees = await dbContext.Employees
                           .Include(o => o.EmployeeContracts)
                           .OrderBy(o => o.FullName.FirstName)
                           .Where(g => g.GymId == query.gymId)
                           .ToListAsync(cancellationToken);

            return new GetEmployeesByGymIdResult(
                [.. Employees.ToEmployeeDtoList()]);
        }
    }
}
