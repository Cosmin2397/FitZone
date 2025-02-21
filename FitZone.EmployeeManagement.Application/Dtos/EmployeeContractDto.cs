using FitZone.EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Dtos
{
    public record EmployeeContractDto(Guid employeeId, DateTime startDate, DateTime endDate, decimal monthlyPayment);
}
