using Discount.Grpc.Repositories;

namespace Discount.Grpc;

public static class ConfigureServices
{
    public static void Config(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.Services.AddGrpc();

        services.AddScoped<IDiscountRepository, DiscountRepository>();
    }
}