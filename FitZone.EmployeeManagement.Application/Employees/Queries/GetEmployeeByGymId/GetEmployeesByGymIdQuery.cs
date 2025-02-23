using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeByGymId
{
    public record GetEmployeesByGymIdQuery(Guid gymId)
   : IQuery<GetEmployeesByGymIdResult>;

    public record GetEmployeesByGymIdResult(List<EmployeeDto> Employees);
}
