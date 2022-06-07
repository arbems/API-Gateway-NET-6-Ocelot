using ApiGatewayOcelot.aggregates;
using ApiGatewayOcelot.handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });

builder.Services.AddOcelot()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
    .AddTransientDefinedAggregator<MyAggregator>()
    .AddDelegatingHandler<NoEncodingHandler>(true)
    .AddDelegatingHandler<ApiKeyHandler>()
    .AddDelegatingHandler<FakeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.Run();

