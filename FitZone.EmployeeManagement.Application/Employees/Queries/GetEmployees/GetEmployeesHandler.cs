using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Extensions;
using Marten.Linq.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
    {
        public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
        {
            // get Employees with 
            // return result

            var Employees = await dbContext.Employees
                                    .OrderBy(o => o.FullName.FirstName)
                                    .ToListAsync(cancellationToken)
                                    .ConfigureAwait(false);

            if (Employees != null && Employees.Count > 0)
            {
                foreach (var employee in Employees)
                {
                    var contracts = await dbContext.EmployeesContracts.Where(e => e.EmployeeId == employee.Id).ToListAsync();
                    if (contracts != null && contracts.Count > 0)
                    {
                        employee.SetCotracts(contracts);
                    }
                }
            }
            return new GetEmployeesResult(
                new List<EmployeeDto>(
                    Employees.ToEmployeeDtoList()));
        }
    }
}
