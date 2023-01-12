using Basket.Api.Repositories;
using Basket.Api.Services;
using MassTransit;
using System.Reflection;
using static Discount.Grpc.DiscountService;

namespace Basket.Api;

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

        // Grpc configuration
        services
            .AddGrpcClient<DiscountServiceClient>(o =>
            {
                o.Address = new Uri(configuration["DiscountServiceHost"]);
            });
        services.AddScoped<DiscountGrpcService>();

        // Redis configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
        });

        // RabbitMq configuration
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["EventBus:Host"]);
            });
        });
        services.AddMassTransitHostedService();

        // automapper configuration
        services.AddAutoMapper(typeof(ConfigureServices));

        // General configuration
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
    }
}