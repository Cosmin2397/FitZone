using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Domain.Models;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using Marten.Linq.Parsing.Operators;
using Microsoft.CodeAnalysis;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.AddEmployee
{
    public class AddEmployeeHandler(IApplicationDbContext dbContext)
     : ICommandHandler<AddEmployeeCommand, AddEmployeeResult>
    {
        public async Task<AddEmployeeResult> Handle(AddEmployeeCommand command, CancellationToken cancellationToken)
        {
            //create employee entity from command object
            //save to database
            //return result 

            var employee = CreateNewEmployee(command.Employee);

            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new AddEmployeeResult(employee.Id.Value);
        }

        private Employee CreateNewEmployee(EmployeeDto employeeDto)
        {
            var fullnameCreated = FullName.Of(employeeDto.fullName.firstName, employeeDto.fullName.middleName, employeeDto.fullName.lastName);
            var phoneNumberCreated = PhoneNumber.Of(employeeDto.phoneNumber.countryCode, employeeDto.phoneNumber.remainingLetters);

            var newEmployee = Employee.Create(
                    id: EmployeeId.Of(Guid.NewGuid()),
                    gymId: employeeDto.gymId,
                    roleId: employeeDto.roleId,
                    fullName: fullnameCreated,
                    phoneNumber: phoneNumberCreated,
                    birthday: employeeDto.birthday
            );

            foreach (var employeeContract in employeeDto.employeeContracts)
            {
                newEmployee.Add(EmployeeContractId.Of(employeeContract.employeeContractId), employeeContract.startDate,employeeContract.monthlyPayment);
            }
            return newEmployee;
        }
    }
}
