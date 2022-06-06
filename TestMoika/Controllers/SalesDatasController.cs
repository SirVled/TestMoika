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
    public class SalesDatasController : ControllerBase
    {
        private readonly Context _context;

        public SalesDatasController(Context context)
        {
            _context = context;
        }

        // GET: api/SalesDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesData>>> GetSalesDates()
        {
          if (_context.SalesDates == null)
          {
              return NotFound();
          }
            return await _context.SalesDates.ToListAsync();
        }

        // GET: api/SalesDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesData>> GetSalesData(int id)
        {
          if (_context.SalesDates == null)
          {
              return NotFound();
          }
            var salesData = await _context.SalesDates.FindAsync(id);

            if (salesData == null)
            {
                return NotFound();
            }

            return salesData;
        }

        // PUT: api/SalesDatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesData(int id, SalesData salesData)
        {
            if (id != salesData.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesDataExists(id))
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

        // POST: api/SalesDatas
        [HttpPost]
        public async Task<ActionResult<SalesData>> PostSalesData(SalesData salesData)
        {
          if (_context.SalesDates == null)
          {
              return Problem("Entity set 'Context.SalesDates'  is null.");
          }
            _context.SalesDates.Add(salesData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesData", new { id = salesData.Id }, salesData);
        }

        // DELETE: api/SalesDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesData(int id)
        {
            if (_context.SalesDates == null)
            {
                return NotFound();
            }
            var salesData = await _context.SalesDates.FindAsync(id);
            if (salesData == null)
            {
                return NotFound();
            }

            _context.SalesDates.Remove(salesData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesDataExists(int id)
        {
            return (_context.SalesDates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
