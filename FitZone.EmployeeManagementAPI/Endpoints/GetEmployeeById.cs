using Carter;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.EmployeeManagementAPI.Endpoints
{
    public record GetEmployeeByIdResponse(EmployeeDto EmployeeDto);

    public class GetEmployeeById : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/employee/{id}", async ([FromRoute] Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetEmployeeByIdQuery(id));

                var response = result;

                return Results.Ok(response);
            })
            .WithName("GetEmployeeById")
            .Produces<GetEmployeeByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Employee By  Id")
            .WithDescription("Get Employee By Id");
        }
    }
}
