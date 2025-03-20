using FitZone.SubscriptionService.Shared.Data;
using FitZone.SubscriptionService.Shared.Domain.DTOs.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FitZone.SubscriptionService.RabbitMQ
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
            channel.ExchangeDeclare(exchange: "user.events", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: "user.events", routingKey: "user.deleted");

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

                    var subscriptions = await context.Subscriptions
                        .Where(i => i.ClientId == userDeleted.Id && i.Status != Shared.Domain.Enums.Status.Canceled)
                        .ToListAsync();

                    if (subscriptions.Any())
                    {
                        subscriptions.ForEach(s =>
                        {
                            s.Status = Shared.Domain.Enums.Status.Canceled;
                            s.EndDate = DateTime.Now;
                        });

                        var result = await context.SaveChangesAsync();
                        if (result > 0)
                        {
                            Console.WriteLine($"Subscriptiile utilizatorului sters au fost anulate cu succes: {userDeleted.Id}");
                        }
                        else
                        {
                            Console.WriteLine($"Eroare la anularea subscriptiilor utilizatorului: {userDeleted.Id}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Utilizatorul: {userDeleted.Id} nu are subscriptii valide");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceptie la anularea subscriptiilor: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

}
