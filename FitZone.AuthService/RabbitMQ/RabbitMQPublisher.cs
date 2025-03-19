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

        public void Publish<T>(string exchange, string routingKey, T message)
        {
            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
        }
    }
}
