using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace FitZone.AuthentificationService.RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly IConnection _connection;

        public RabbitMQPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public void Publish<T>(string exchange, T message)
        {
            using var channel = _connection.CreateModel();

            // Declară exchange-ul de tip fanout
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"[x] Sent message to exchange: {exchange}");
        }
    }

}
