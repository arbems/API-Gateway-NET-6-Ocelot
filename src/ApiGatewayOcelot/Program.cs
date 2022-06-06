using ApiGatewayOcelot.aggregates;
using ApiGatewayOcelot.handlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddOcelot()
    .AddTransientDefinedAggregator<MyAggregator>()
    .AddDelegatingHandler<NoEncodingHandler>(true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.Run();

