﻿using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;

namespace StoreLibrary.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context = new();

        public async Task<List<Order>> GetAllOrdersAsync()
            => await _context.Orders.ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(int id)
            => await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}