using MQ.Sender.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace MQ.Sender.Services
{
    internal class Sender : BackgroundService
    {
        private readonly IDateTimeService _dateTimeService;

        public Sender(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            while (!stoppingToken.IsCancellationRequested)
            {
                string message = $"{_dateTimeService.Now()} - Hello World!";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
