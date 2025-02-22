using FitZone.EmployeeManagement.Application.CQRS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(Guid employeeId)
    : ICommand<DeleteEmployeeResult>;

    public record DeleteEmployeeResult(bool IsSuccess);

    public class DeleteEmployeeCommandValidatior : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidatior()
        {
            RuleFor(x => x.employeeId).NotEmpty().WithMessage("EmployeeId is required");
        }
    }
}
