﻿using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AspnetRunBasics.Entities;
using AspnetRunBasics.Helpers;

namespace AspnetRunBasics.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _client;

        public ProductRepository(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<Product[]> GetProducts()
        {
            var response = await _client.GetAsync("/Catalog");
            return await response.ReadContentAs<Product[]>();
        }

        public async Task<Product> GetProductById(string id)
        {
            var response = await _client.GetAsync($"/Catalog/{id}");
            return await response.ReadContentAs<Product>();
        }

        public async Task<Product[]> GetProductByCategory(string categoryName)
        {
            var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{categoryName}");
            return await response.ReadContentAs<Product[]>();
        }

        public async Task<Product> AddAsync(Product product)
        {
            var response = await _client.PostAsync("/Catalog", product);
            var result = await response.ReadContentAs<Product>();
            return result;
        }

        public async Task UpdateAsync(Product product)
        {
            await _client.PutAsync("/Catalog", product);
        }

        public async Task DeleteAsync(string id)
        {
            await _client.DeleteAsync($"/Catalog/{id}");
        }

        public async Task<Category[]> GetCategories()
        {
            var products = await GetProducts();
            return products.Select(p => p.Category).Distinct()
                .Select(name => new Category { Name = name }).ToArray();
        }
    }
}