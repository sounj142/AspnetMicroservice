using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Ordering.Api.EventBusConsumers;
using Ordering.Api.Services;
using Ordering.Application;
using Ordering.Application.Contracts;
using Ordering.Application.Models;
using Ordering.Infrastructure;

namespace Ordering.Api;

public static class ConfigureServices
{
    public static void Config(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // Web api configuration
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            // disable default Validation failure response mechanism
            options.SuppressModelStateInvalidFilter = true;
        });

        // RabbitMq configuration
        services.AddMassTransit(config =>
        {
            config.AddConsumer<BasketCheckoutConsumer>();

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBus:Host"]);
                cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                {
                    c.ConfigureConsumer<BasketCheckoutConsumer>(context);
                });
            });
        });
        services.AddMassTransitHostedService();
        services.AddScoped<BasketCheckoutConsumer>();

        // AutoMapper configuration
        services.AddAutoMapper(typeof(ConfigureServices));

        // libraries configuration
        services.AddApplicationServices();
        services.AddInfrastructureServices(
            configuration.GetConnectionString("DefaultConnection"));

        // General configuration
        services.AddSingleton(
            configuration.GetSection("EmailSettings").Get<EmailSettings>());

        services.AddHttpContextAccessor();
        services.AddScoped<ManualCurrentUserContext>();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
    }
}