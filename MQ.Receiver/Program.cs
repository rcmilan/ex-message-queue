using MQ.Receiver.MessageReceivers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<HelloReceiverA>();
        services.AddHostedService<HelloReceiverB>();
    })
    .Build();

await host.RunAsync();

// Importante: <Project Sdk="Microsoft.NET.Sdk.Worker">