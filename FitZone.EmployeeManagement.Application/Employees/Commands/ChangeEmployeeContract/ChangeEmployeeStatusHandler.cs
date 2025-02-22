using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using FitZone.EmployeeManagement.Application.Exceptions;
using FitZone.EmployeeManagement.Domain.Models;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.ChangeEmployeeContract
{
    public class ChangeEmployeeStatusHandler(IApplicationDbContext dbContext)
     : ICommandHandler<ChangeEmployeeStatusCommand, ChangeEmployeeStatusResult>
    {
        public async Task<ChangeEmployeeStatusResult> Handle(ChangeEmployeeStatusCommand command, CancellationToken cancellationToken)
        {
            //Update Employee and delete contracts status entity from command object
            //save to database
            //return result

            var employeeId = EmployeeId.Of(command.Employee.id.Value);
            var employee = await dbContext.Employees
                .FindAsync([employeeId], cancellationToken: cancellationToken);


            if (employee is null)
            {
                throw new EmployeeNotFoundException(command.Employee.id.Value);
            }

            var contracts = employee.EmployeeContracts.Where(d=> d.EndDate.Date > DateTime.Now.Date);

            ChangeEmployeeStatusWithNewValue(employee, contracts, command.Employee);

            dbContext.Employees.Update(employee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ChangeEmployeeStatusResult(true);
        }

        public void ChangeEmployeeStatusWithNewValue(Employee employee, IEnumerable<EmployeeContract> contracts, ChangeEmployeeStatusDTO employeeDto)
        {

            employee.UpdateStatus(employeeDto.status);

            if(employee.EmployeeContracts != null && employee.EmployeeContracts.Count > 0)
            {
                foreach(var contract in contracts)
                {
                    employee.Remove(contract.Id);
                }
            }
        }
    }
}
