using Shopping.AggregatorApi.Services;

namespace Shopping.AggregatorApi;

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

        // automapper configuration
        services.AddAutoMapper(typeof(ConfigureServices));

        // http client configuration
        services.AddHttpClient<CatalogService>(
            client => client.BaseAddress = new Uri(configuration["Apis:CatalogApi"]));
        services.AddHttpClient<BasketService>(
            client => client.BaseAddress = new Uri(configuration["Apis:BasketApi"]));
        services.AddHttpClient<OrderService>(
            client => client.BaseAddress = new Uri(configuration["Apis:OrderApi"]));
    }
}