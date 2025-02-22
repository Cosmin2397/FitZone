using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Exceptions;
using FitZone.EmployeeManagement.Domain.Models;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using FluentValidation;
using Marten.Linq.Parsing.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler(IApplicationDbContext dbContext)
     : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResult>
    {
        public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            //Update Employee entity from command object
            //save to database
            //return result

            var employeeId = EmployeeId.Of(command.Employee.id.Value);
            var employee = await dbContext.Employees
                .FindAsync([employeeId], cancellationToken: cancellationToken);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(command.Employee.id.Value);
            }

            UpdateEmployeeWithNewValues(employee, command.Employee);

            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateEmployeeResult(true);
        }

        public void UpdateEmployeeWithNewValues(Employee employee, EmployeeDto employeeDto)
        {
            var updatedFullname = FullName.Of(employeeDto.fullName.firstName, employeeDto.fullName.middleName, employeeDto.fullName.lastName);
            var updatedPhoneNumber = PhoneNumber.Of(employeeDto.phoneNumber.countryCode, employeeDto.phoneNumber.remainingLetters);

            if(employee.RoleId != employeeDto.roleId)
            {
                var contracts = employee.EmployeeContracts.Where(d => d.EndDate.Date > DateTime.Now.Date);
                if (employee.EmployeeContracts != null && employee.EmployeeContracts.Count > 0)
                {
                    foreach (var contract in contracts)
                    {
                        employee.Remove(contract.Id);
                    }
                    employee.Add(EmployeeContractId.Of(employeeDto.employeeContracts.FirstOrDefault().employeeContractId), DateTime.Now, employeeDto.employeeContracts.FirstOrDefault().monthlyPayment);
                }

            }

            employee.Update(
                gymId: employeeDto.gymId,
                roleId: employeeDto.roleId,
                fullName: updatedFullname,
                phoneNumber: updatedPhoneNumber,
                birthday: employeeDto.birthday
            );

        }
    }
}
