using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetProducts();

    Task<IList<Product>> GetProducts(string[] ids);

    Task<Product?> GetProduct(string id);

    Task<IList<Product>> SearchProductsByName(string name);

    Task<IList<Product>> GetProductsByCategoryName(string categoryName);

    Task<Product> CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string id);
}