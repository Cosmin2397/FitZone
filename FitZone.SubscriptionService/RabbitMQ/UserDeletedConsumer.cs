using FitZone.SubscriptionService.Shared.Domain.DTOs.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FitZone.SubscriptionService.RabbitMQ
{
    public class UserDeletedConsumer : BackgroundService
    {
        private readonly IConnection _connection;

        public UserDeletedConsumer(IConnection connection)
        {
            _connection = connection;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: "user.events", type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: "user.events", routingKey: "user.deleted");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var userDeleted = JsonSerializer.Deserialize<UserDeletedEvent>(message);
                Console.WriteLine($"User deleted: {userDeleted.Id}");
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }

}
