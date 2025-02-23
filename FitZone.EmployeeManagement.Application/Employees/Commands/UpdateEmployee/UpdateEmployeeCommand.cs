using FitZone.EmployeeManagement.Application.CQRS;
using FitZone.EmployeeManagement.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(EmployeeDto Employee)
   : ICommand<UpdateEmployeeResult>;

    public record UpdateEmployeeResult(bool IsSuccess);

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee.fullName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Employee.phoneNumber).NotNull().WithMessage("Phone Number is required");
            RuleFor(x => x.Employee.employeeContracts).NotEmpty().WithMessage("It should Be at least one employee contract");
            RuleFor(x => x.Employee.birthday).NotEmpty().WithMessage("Birthday is required");
            RuleFor(x => x.Employee.status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.Employee.gymId).NotEmpty().WithMessage("GymId is required");
            RuleFor(x => x.Employee.roleId).NotEmpty().WithMessage("RoleId is required");
        }
    }
}
