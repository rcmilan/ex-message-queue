using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MQ.Receiver.Consumers
{
    internal class HelloConsumerB : BackgroundService
    {
        private const string HOST_NAME = "ex-rabbit";
        private const string QUEUE_NAME = "hello";

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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(" [receiver b] Received {0}", message.ToLower());
            };

            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(1, stoppingToken);
        }
    }
}
