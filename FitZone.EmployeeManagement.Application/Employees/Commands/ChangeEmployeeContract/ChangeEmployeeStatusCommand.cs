using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.ChangeEmployeeContract
{
    public record ChangeEmployeeStatusCommand(ChangeEmployeeStatusDTO Employee)
    : ICommand<ChangeEmployeeStatusResult>;

    public record ChangeEmployeeStatusResult(bool IsSuccess);

    public class ChangeEmployeeStatusValidator : AbstractValidator<ChangeEmployeeStatusCommand>
    {
        public ChangeEmployeeStatusValidator()
        {
            RuleFor(x => x.Employee.id).NotEmpty().WithMessage("Employee id is required");
            RuleFor(x => x.Employee.status).NotEmpty().WithMessage("Status is required");
        }
    }
}
