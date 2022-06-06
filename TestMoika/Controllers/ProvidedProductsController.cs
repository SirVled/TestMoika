using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMoika.Data;
using TestMoika.Entites;

namespace TestMoika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidedProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProvidedProductsController(Context context)
        {
            _context = context;
        }

        // GET: api/ProvidedProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvidedProduct>>> GetProvidedProducts()
        {
          if (_context.ProvidedProducts == null)
          {
              return NotFound();
          }
            return await _context.ProvidedProducts.ToListAsync();
        }

        // GET: api/ProvidedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvidedProduct>> GetProvidedProduct(int id)
        {
          if (_context.ProvidedProducts == null)
          {
              return NotFound();
          }
            var providedProduct = await _context.ProvidedProducts.FindAsync(id);

            if (providedProduct == null)
            {
                return NotFound();
            }

            return providedProduct;
        }

        // PUT: api/ProvidedProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvidedProduct(int id, ProvidedProduct providedProduct)
        {
            if (id != providedProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(providedProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvidedProductExists(id))
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

        // POST: api/ProvidedProducts
        [HttpPost]
        public async Task<ActionResult<ProvidedProduct>> PostProvidedProduct(ProvidedProduct providedProduct)
        {
          if (_context.ProvidedProducts == null)
          {
              return Problem("Entity set 'Context.ProvidedProducts'  is null.");
          }
            _context.ProvidedProducts.Add(providedProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvidedProduct", new { id = providedProduct.Id }, providedProduct);
        }

        // DELETE: api/ProvidedProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvidedProduct(int id)
        {
            if (_context.ProvidedProducts == null)
            {
                return NotFound();
            }
            var providedProduct = await _context.ProvidedProducts.FindAsync(id);
            if (providedProduct == null)
            {
                return NotFound();
            }

            _context.ProvidedProducts.Remove(providedProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvidedProductExists(int id)
        {
            return (_context.ProvidedProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
