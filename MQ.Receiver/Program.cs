using MQ.Receiver.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<HelloConsumerA>();
        services.AddHostedService<HelloConsumerB>();
    })
    .Build();

await host.RunAsync();

// Importante: <Project Sdk="Microsoft.NET.Sdk.Worker">