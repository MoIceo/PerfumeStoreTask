using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;

namespace StoreLibrary.Services
{
    public class PickupPointService
    {
        private readonly AppDbContext _context = new();

        public async Task<List<PickupPoint>> GetAllPickupPointsAsync()
            => await _context.PickupPoints.ToListAsync();

        public async Task<PickupPoint?> GetPickupPointByIdAsync(int id)
            => await _context.PickupPoints.FirstOrDefaultAsync(p => p.PickupPointId == id);

        public async Task AddPickupPointAsync(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Add(pickupPoint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePickupPointAsync(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Update(pickupPoint);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePickupPointAsync(PickupPoint pickupPoint)
        {
            _context.PickupPoints.Remove(pickupPoint);
            await _context.SaveChangesAsync();
        }
    }
}