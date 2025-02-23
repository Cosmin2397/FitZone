using Carter;
using FitZone.EmployeeManagement.Application.Data;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployees;
using MediatR;
using Marten;

namespace FitZone.EmployeeManagementAPI.Endpoints
{

    public record GetEmployeesResponse(PaginatedResult<EmployeeDto> EmployeeDto);

    public class GetEmployees: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/employees", async (ISender sender) =>
            {
                var result = await sender.Send(new GetEmployeesQuery());

                var response = result;

                return Results.Ok(response);
            })
            .WithName("GetEmployees")
            .Produces<GetEmployeesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Employees")
            .WithDescription("Get Employees");
        }
    }
}
