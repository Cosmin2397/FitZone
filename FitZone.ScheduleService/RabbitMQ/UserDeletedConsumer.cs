using FitZone.ScheduleService.Data;
using FitZone.ScheduleService.DTOs.RabbitMQ;
using FitZone.ScheduleService.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FitZone.ScheduleService.RabbitMQ
{
    public class UserDeletedConsumer : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConnection _connection;

        public UserDeletedConsumer(IConnection connection, IServiceScopeFactory scopeFactory)
        {
            _connection = connection;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var channel = _connection.CreateModel();

            // Declararea exchange-ului de tip Fanout
            channel.ExchangeDeclare(exchange: "user.events", type: ExchangeType.Fanout);

            // Declararea unui queue unic pentru acest consumator
            var queueName = channel.QueueDeclare(queue: "user.deleted.queue", durable: true, exclusive: false, autoDelete: false).QueueName;

            // Leagă queue-ul la exchange, fără routing key
            channel.QueueBind(queue: queueName, exchange: "user.events", routingKey: "");

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var userDeleted = JsonSerializer.Deserialize<UserDeletedEvent>(message);

                    Console.WriteLine($"User deleted event read: {userDeleted.Id}");

                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var trainings = await context.Trainings.Include(x => x.ScheduledClients)
                        .Where(z => z.TrainingStatus == Status.Created && z.TrainerId == userDeleted.Id)
                        .ToListAsync();

                    var schedules = await context.TrainingSchedules
                        .Where(i => i.ClientId == userDeleted.Id && i.ScheduleStatus == TrainingScheduleStatus.Accepted)
                        .ToListAsync();

                    if (trainings.Any())
                    {
                        trainings.ForEach(s =>
                        {
                            s.TrainingStatus = Status.Canceled;
                            s.ScheduledClients.ForEach(sc => sc.ScheduleStatus = TrainingScheduleStatus.TrainingCanceled);
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Utilizatorul: {userDeleted.Id} nu are antrenamente programate valide");
                    }

                    if (schedules.Any())
                    {
                        schedules.ForEach(s => s.ScheduleStatus = TrainingScheduleStatus.Canceled);
                    }
                    else
                    {
                        Console.WriteLine($"Utilizatorul: {userDeleted.Id} nu are programari valide");
                    }

                    var result = await context.SaveChangesAsync();
                    if (result > 0)
                    {
                        Console.WriteLine($"Stergerea antrenamentelor/programarilor a fost cu succes: {userDeleted.Id}");
                    }
                    else
                    {
                        Console.WriteLine($"Eroare la anularea antrenamentelor/programarilor utilizatorului: {userDeleted.Id}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceptie la anularea antrenamentelor/programarilor: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}