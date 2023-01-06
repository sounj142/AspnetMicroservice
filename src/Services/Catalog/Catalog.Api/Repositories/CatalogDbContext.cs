using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class CatalogDbContext
{
    public IMongoCollection<Product> Products { get; }

    public CatalogDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);
    }
}