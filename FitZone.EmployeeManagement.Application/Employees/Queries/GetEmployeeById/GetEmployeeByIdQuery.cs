using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(Guid id)
  : IQuery<GetEmployeeByIdResult>;

    public record GetEmployeeByIdResult(EmployeeDto Employee);
}
