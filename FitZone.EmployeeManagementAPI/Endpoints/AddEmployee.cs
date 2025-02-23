using Carter;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Commands.AddEmployee;
using Mapster;
using MediatR;

namespace FitZone.EmployeeManagementAPI.Endpoints
{
    public record AddEmployeeRequest(EmployeeDto Employee);
    public record AddEmployeeResponse(Guid Id);

    public class AddEmployee : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/employees", async (AddEmployeeRequest request, ISender sender) =>
            {
                var command = new AddEmployeeCommand(request.Employee);

                var result = await sender.Send(command);

                var response = new AddEmployeeResponse(result.Id);

                return Results.Created($"/employees/{response.Id}", response);
            })
            .WithName("AddEmployee")
            .Produces<AddEmployeeResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Add Employee")
            .WithDescription("Add Employee");
        }
    }
}
