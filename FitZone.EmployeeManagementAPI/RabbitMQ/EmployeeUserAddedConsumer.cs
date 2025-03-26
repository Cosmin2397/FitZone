using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitZone.EmployeeManagement.Application.Dtos;
using System.Text.Json;
using FitZone.EmployeeManagement.Application.Employees.Commands.AddEmployee;
using MediatR;

namespace FitZone.EmployeeManagement.Application.RabbitMQ
{
    public class EmployeeUserAddedConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly ISender _sender;
        private readonly IServiceProvider _serviceProvider;

        public EmployeeUserAddedConsumer(IConnection connection, ISender sender, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _sender = sender;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var channel = _connection.CreateModel();

            // Declararea exchange-ului de tip Fanout
            channel.ExchangeDeclare(exchange: "employee.events", type: ExchangeType.Fanout);

            // Declararea unui queue unic pentru acest consumator
            var queueName = channel.QueueDeclare(queue: "employee.events.employee.queue", durable: true, exclusive: false, autoDelete: false).QueueName;

            // Leagă queue-ul la exchange, fără routing key
            channel.QueueBind(queue: queueName, exchange: "employee.events", routingKey: "");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var employeeData = JsonSerializer.Deserialize<JsonElement>(message);
                    Guid randomEmployeeContract = Guid.NewGuid();
                    var employeeDto = new EmployeeDto(
                        employeeData.GetProperty("Id").GetGuid(),
                        employeeData.GetProperty("GymId").GetGuid(),
                        employeeData.GetProperty("RoleId").GetGuid(),
                        new FullNameDto(employeeData.GetProperty("FirstName").GetString(), "", employeeData.GetProperty("LastName").GetString()),
                        new PhoneNumberDto(040, Convert.ToInt32(employeeData.GetProperty("PhoneNumber").GetString())),
                        employeeData.GetProperty("Birthday").GetDateTime(),
                        "Active", 
                        new List<EmployeeContractDto>
                        {
                        new EmployeeContractDto(
                            randomEmployeeContract,
                            employeeData.GetProperty("Id").GetGuid(),
                            employeeData.GetProperty("StartDate").GetDateTime(),
                            employeeData.GetProperty("EndDate").GetDateTime(),
                            employeeData.GetProperty("MonthlyPayment").GetDecimal()
                        )
                                        }
                                    );

                    Console.WriteLine(JsonSerializer.Serialize(employeeDto, new JsonSerializerOptions { WriteIndented = true }));

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var command = new AddEmployeeCommand(employeeDto);
                        var result = await mediator.Send(command);

                        if (result.Id != null)
                        {
                            Console.WriteLine($"Angajatul {employeeDto.fullName} a fost adaugat cu succes");
                        }
                        else
                        {
                            Console.WriteLine($"Angajatul {employeeDto.fullName} nu a fost adaugat cu succes");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceptie la adaugarea angajatului: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
