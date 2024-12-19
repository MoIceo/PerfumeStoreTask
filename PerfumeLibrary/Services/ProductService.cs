using Microsoft.EntityFrameworkCore;
using PerfumeLibrary.Data;
using PerfumeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeLibrary.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context = new();

        public async Task<List<Product>> GetAllProductsAsync()
            => await _context.Products.ToListAsync();

        public async Task<Product?> GetProductByArticleAsync(int article)
            => await _context.Products.FirstOrDefaultAsync(p => p.ProductArticleNumber == article);

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
