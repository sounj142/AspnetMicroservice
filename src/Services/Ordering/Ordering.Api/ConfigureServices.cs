using Microsoft.AspNetCore.Mvc;
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

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            // disable default Validation failure response mechanism
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddApplicationServices();
        services.AddInfrastructureServices(
            configuration.GetConnectionString("DefaultConnection"));

        services.AddSingleton(
            configuration.GetSection("EmailSettings").Get<EmailSettings>());

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
    }
}