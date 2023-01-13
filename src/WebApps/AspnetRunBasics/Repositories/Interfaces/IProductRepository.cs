using AspnetRunBasics.Entities;
using System.Threading.Tasks;

namespace AspnetRunBasics.Repositories
{
    public interface IProductRepository
    {
        Task<Product[]> GetProducts();

        Task<Product> GetProductById(string id);

        Task<Product[]> GetProductByCategory(string categoryName);

        Task<Category[]> GetCategories();
    }
}