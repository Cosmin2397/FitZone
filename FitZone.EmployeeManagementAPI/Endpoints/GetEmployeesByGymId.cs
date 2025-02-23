using Carter;
using FitZone.EmployeeManagement.Application.Dtos;
using FitZone.EmployeeManagement.Application.Employees.Queries.GetEmployeeByGymId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitZone.EmployeeManagementAPI.Endpoints
{
    public record GetEmployeesByGymIdResponse(List<EmployeeDto> EmployeeDto);

    public class GetEmployeesByGymId : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/employees/{gymId}", async ([FromRoute] Guid gymId, ISender sender) =>
            {
                var result = await sender.Send(new GetEmployeesByGymIdQuery(gymId));

                var response = result;

                return Results.Ok(response);
            })
            .WithName("GetEmployeesByGymId")
            .Produces<GetEmployeesByGymIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Employees By Gym Id")
            .WithDescription("Get Employees By Gym Id");
        }
    }
}
