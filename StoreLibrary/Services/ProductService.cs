using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StoreLibrary.Services
{
    public class ProductService
    {

        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5243/api/";

        public ProductService()
        {
            _client = new() { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync("Products/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }
        public async Task<Product> GetProductByIdAsync(string productArticle)
        {
            var response = await _client.GetAsync($"Products/{productArticle}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task AddProductAsync(Product product)
        {
            var response = await _client.PostAsJsonAsync("Products/", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var response = await _client.PutAsJsonAsync($"Products/{product.ProductArticleNumber}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(string productArticle)
        {
            var response = await _client.DeleteAsync($"Products/{productArticle}");
            response.EnsureSuccessStatusCode();
        }

    }
}