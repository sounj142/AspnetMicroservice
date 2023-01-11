using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Persistence;

public class DatabaseMigration
{
    public static void Migration(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<DatabaseMigration>>();
        try
        {
            var dbContext = services.GetRequiredService<OrderDbContext>();
            dbContext.Database.Migrate();
            logger.LogInformation("Migrated database schema.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during migrate database.");
            throw;
        }
    }
}