using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StoreLibrary.Services
{
    public class OrderService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5243/api/";

        public OrderService()
        {
            _client = new() { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var response = await _client.GetAsync("Orders/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var response = await _client.GetAsync($"Orders/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }

        public async Task AddOrderAsync(Order order)
        {
            var response = await _client.PostAsJsonAsync("Orders/", order);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var response = await _client.PutAsJsonAsync($"Orders/{order.OrderId}", order);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var response = await _client.DeleteAsync($"Orders/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}