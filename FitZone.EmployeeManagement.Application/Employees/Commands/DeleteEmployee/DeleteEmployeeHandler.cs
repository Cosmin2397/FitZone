using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Exceptions;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using FluentValidation;
using Marten.Linq.Parsing.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler(IApplicationDbContext dbContext)
     : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            //Delete Employee entity from command object
            //save to database
            //return result

            var employeeId = EmployeeId.Of(command.employeeId);
            var employee = await dbContext.Employees
                .FindAsync([employeeId], cancellationToken: cancellationToken);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(command.employeeId);
            }

            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteEmployeeResult(true);
        }
    }
}
