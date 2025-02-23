using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery()
    : IQuery<GetEmployeesResult>;

    public record GetEmployeesResult(List<EmployeeDto> Employees);
}
