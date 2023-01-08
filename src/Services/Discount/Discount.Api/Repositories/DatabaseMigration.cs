using Npgsql;
using Dapper;

namespace Discount.Api.Repositories;

public class DatabaseMigration
{
    public static void Migration(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<DatabaseMigration>>();
        try
        {
            using var connection = new NpgsqlConnection
                        (configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };

            var currentTable = connection.QueryFirstOrDefault<object>(
                @"SELECT * FROM information_schema.tables
  	            WHERE table_name = 'coupon'");
            if (currentTable == null)
            {
                logger.LogInformation("Migrating postresql database.");

                command.CommandText = @"CREATE TABLE coupon(Id SERIAL PRIMARY KEY,
                                    ProductName VARCHAR(400) NOT NULL,
                                    Description TEXT,
                                    Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();

                logger.LogInformation("Migrated postresql database.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during migrate database");
            throw;
        }
    }
}