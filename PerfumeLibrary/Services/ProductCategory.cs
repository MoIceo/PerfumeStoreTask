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
    public class ProductCategoryCategory
    {
        private readonly AppDbContext _context = new();

        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
            => await _context.ProductCategories.ToListAsync();

        public async Task<ProductCategory?> GetProductCategoryByIdAsync(int id)
            => await _context.ProductCategories.FirstOrDefaultAsync(p => p.ProductCategoryId == id);

        public async Task AddProductCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductCategoryAsync(ProductCategory category)
        {
            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
