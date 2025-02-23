using Carter;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Commands.UpdateEmployee;
using Mapster;
using MediatR;

namespace FitZone.EmployeeManagementAPI.Endpoints
{
    public record UpdateEmployeeRequest(EmployeeDto Employee);
    public record UpdateEmployeeResponse(bool IsSuccess);

    public class UpdateEmployee : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/updateemployee", async (UpdateEmployeeRequest request, ISender sender) =>
            {
                var command = new UpdateEmployeeCommand(request.Employee);

                var result = await sender.Send(command);

                var response = new UpdateEmployeeResponse(result.IsSuccess);

                return Results.Ok(response);
            })
            .WithName("UpdateEmployee")
            .Produces<UpdateEmployeeResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Employee")
            .WithDescription("Update Employee");
        }
    }
}
