using MQ.Sender.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace MQ.Sender.MessageSenders
{
    internal class HelloSender : BackgroundService
    {
        private const string HOST_NAME = "rabbitmq";
        private const string QUEUE_NAME = "hello";

        private readonly IDateTimeService _dateTimeService;
        public HelloSender(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = HOST_NAME };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: QUEUE_NAME,
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