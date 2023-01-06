using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProduct(string id)
    {
        return await _dbContext.Products.Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IList<Product>> GetProducts()
    {
        return await _dbContext.Products.Find(p => true)
            .ToListAsync();
    }

    public async Task<IList<Product>> SearchProductsByName(string name)
    {
        //!!!!!!!!!!!!
        var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
        return await _dbContext.Products.Find(filter)
            .ToListAsync();
    }

    public async Task<IList<Product>> GetProductsByCategoryName(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
        return await _dbContext.Products.Find(filter)
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _dbContext.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _dbContext.Products
            .ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var result = await _dbContext.Products
            .DeleteOneAsync(p => p.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}