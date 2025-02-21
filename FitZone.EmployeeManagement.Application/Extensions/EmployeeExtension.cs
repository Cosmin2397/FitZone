using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Extensions
{
    public static class EmployeeExtensions
    {
        public static IEnumerable<EmployeeDto> ToEmployeeDtoList(this IEnumerable<Employee> employees)
        {
            return employees.Select(employee => DtoFromEmployee(employee));
        }

        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return DtoFromEmployee(employee);
        }

        private static EmployeeDto DtoFromEmployee(Employee employee)
        {
            return new EmployeeDto(
                id: employee.Id,
                gymId: employee.GymId,
                roleId: employee.RoleId,
                fullName: new FullNameDto(
                    employee.FullName.FirstName,
                    employee.FullName.MiddleName,
                    employee.FullName.LastName
                ),
                phoneNumber: new PhoneNumberDto(
                    employee.PhoneNumber.CountryCode,
                    employee.PhoneNumber.RemainingLetters
                ),
                birthday: employee.Birthday,
                status: employee.Status,
                employeeContracts: employee.EmployeeContracts.Select(contract => new EmployeeContractDto(
                    employeeId: contract.EmployeeId.Value,
                    startDate: contract.StartDate,
                    endDate: contract.EndDate,
                    monthlyPayment: contract.MonthlyPayment
                )).ToList()
            );
        }
    }
}
