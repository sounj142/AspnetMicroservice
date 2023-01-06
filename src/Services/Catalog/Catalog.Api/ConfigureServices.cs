using Catalog.Api.Repositories;

namespace Catalog.Api;

public static class ConfigureServices
{
    public static void Config(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<CatalogDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}