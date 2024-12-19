using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;

namespace StoreLibrary.Services
{
    public class UserService
    {
        private readonly AppDbContext _context = new();

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

