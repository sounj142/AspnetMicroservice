using Basket.Api.Repositories;
using Basket.Api.Services;
using static Discount.Grpc.DiscountService;

namespace Basket.Api;

public static class ConfigureServices
{
    public static void Config(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddGrpcClient<DiscountServiceClient>(o =>
            {
                o.Address = new Uri(configuration["DiscountServiceHost"]);
            });
        services.AddScoped<DiscountGrpcService>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
        });

        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
    }
}