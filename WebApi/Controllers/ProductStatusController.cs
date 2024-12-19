using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Data;
using StoreLibrary.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> GetProductStatuses()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStatus>> GetProductStatus(int id)
        {
            var productStatus = await _context.ProductStatuses.FindAsync(id);

            if (productStatus == null)
            {
                return NotFound();
            }

            return productStatus;
        }

        // PUT: api/ProductStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStatus(int id, ProductStatus productStatus)
        {
            if (id != productStatus.ProductStatusId)
            {
                return BadRequest();
            }

            _context.Entry(productStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> PostProductStatus(ProductStatus productStatus)
        {
            _context.ProductStatuses.Add(productStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductStatus", new { id = productStatus.ProductStatusId }, productStatus);
        }

        // DELETE: api/ProductStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStatus(int id)
        {
            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(productStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductStatusExists(int id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
