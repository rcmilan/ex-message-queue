
using MQ.Sender.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Sender>();

    })
    .Build();

await host.RunAsync();

// Importante: <Project Sdk="Microsoft.NET.Sdk.Worker">