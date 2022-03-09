﻿using MQ.Sender.Interfaces;
using MQ.Sender.MessageSenders;
using MQ.Sender.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IDateTimeService, DateTimeService>();

        services.AddHostedService<HelloSender>();
    })
    .Build();

await host.RunAsync();

// Importante: <Project Sdk="Microsoft.NET.Sdk.Worker">