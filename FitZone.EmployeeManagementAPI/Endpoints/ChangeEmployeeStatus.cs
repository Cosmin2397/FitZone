using Carter;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Commands.ChangeEmployeeContract;
using Mapster;
using MediatR;

namespace FitZone.EmployeeManagementAPI.Endpoints
{
    public record ChangeEmployeeStatusEmployeeRequest(ChangeEmployeeStatusDTO Employee);
    public record ChangeEmployeeStatusResponse(bool IsSuccess);

    public class ChangeEmployeeStatus : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/employeeStatus", async (ChangeEmployeeStatusEmployeeRequest request, ISender sender) =>
            {
                var command = new ChangeEmployeeStatusCommand(request.Employee);

                var result = await sender.Send(command);

                var response = new ChangeEmployeeStatusResponse(result.IsSuccess) ;

                return Results.Ok(response);
            })
            .WithName("ChangeEmployeeStatus")
            .Produces<ChangeEmployeeStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Change Employee Status")
            .WithDescription("Change Employee Status");
        }
    }
}
