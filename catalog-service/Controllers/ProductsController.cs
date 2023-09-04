using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using catalog_service.Data;
using catalog_service.Model;
using shared.Events;
using catalog_service_api.Events;
using catalog_service_api.Product;

namespace catalog_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Publisher publisher;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(ApplicationDbContext context, Publisher publisher, ILogger<ProductsController> logger)
        {
            _context = context;
            this.publisher = publisher;
            this.logger = logger;
        }

        // GET: api/Products
        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }

            var headerStr = "";
          foreach(var header in HttpContext.Request.Headers)
            {
                headerStr += $"{header.Key} - {header.Value}\r\n";
            }

            logger.LogWarning($"Headers: {headerStr}");
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "NewProduct")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> PatchPrice(Guid id, ProductPatchDTO dto) 
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return this.BadRequest($"Product with ID {id} not found");

            var message = new ProductPriceChanged
            {
                EntityId = id,
                OldValue = product.Price
            };

            product.Price = dto.Price;
            await _context.SaveChangesAsync();
            message.NewValue = dto.Price;

            try {
              await publisher.PublishAsync(message);
            } catch (Exception e) {
              System.Console.WriteLine(e);
            }
          
            return this.NoContent();
        }
    }
}
