using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MQ.Receiver.MessageReceivers
{
    internal class HelloReceiverB : BackgroundService
    {
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine(" [receiver b] Received {0}", message.ToLower());
            };

            channel.BasicConsume(queue: "hello",
                                 autoAck: true,
                                 consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
                await Task.Delay(1, stoppingToken);
        }
    }
}
