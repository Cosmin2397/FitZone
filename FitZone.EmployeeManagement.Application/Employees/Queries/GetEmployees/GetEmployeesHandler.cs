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
            // get Employees with pagination
            // return result

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Employees.LongCountAsync(cancellationToken);

            var Employees = await dbContext.Employees
                           .Include(o => o.EmployeeContracts)
                           .OrderBy(o => o.FullName.FirstName)
                           .Skip(pageSize * pageIndex)
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

            return new GetEmployeesResult(
                new PaginatedResult<EmployeeDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    Employees.ToEmployeeDtoList()));
        }
    }
}
