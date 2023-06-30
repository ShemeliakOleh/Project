using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Producer;

public class RabbitMqProducer
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqProducer(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "ScrapingResultsQueue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void PublishMessage<T>(T message)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        _channel.BasicPublish(exchange: "",
                              routingKey: "ScrapingResultsQueue",
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}