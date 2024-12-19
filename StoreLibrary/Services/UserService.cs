using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StoreLibrary.Services
{
    public class UserService
    {
        private readonly AppDbContext _context = new();

        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5243/api/";

        public UserService()
        {
            _client = new() { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<User> AuthenticateUserAsync(string login, string password)
        {
            try
            {
                var response = await _client.GetAsync($"Users/login?login={login}&password={password}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<User>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync("Products/");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        }

        public async Task<List<User>> GetAllUsersAsync()
            => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

