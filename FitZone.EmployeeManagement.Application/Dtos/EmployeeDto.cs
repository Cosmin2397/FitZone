using FitZone.EmployeeManagement.Domain.Enums;
using FitZone.EmployeeManagement.Domain.Models;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Dtos
{
    public record EmployeeDto(EmployeeId id, Guid gymId, Guid roleId, FullNameDto fullName, PhoneNumberDto phoneNumber, DateTime birthday,EmployeeStatus status, List<EmployeeContractDto> employeeContracts);
}
