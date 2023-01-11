using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string? orderDatabaseConnectionString)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(orderDatabaseConnectionString));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}