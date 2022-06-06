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
    public class SalesPointsController : ControllerBase
    {
        private readonly Context _context;

        public SalesPointsController(Context context)
        {
            _context = context;
        }

        // GET: api/SalesPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPoint>>> GetSalesPoints()
        {
          if (_context.SalesPoints == null)
          {
              return NotFound();
          }
            return await _context.SalesPoints.ToListAsync();
        }

        // GET: api/SalesPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPoint>> GetSalesPoint(int id)
        {
          if (_context.SalesPoints == null)
          {
              return NotFound();
          }
            var salesPoint = await _context.SalesPoints.FindAsync(id);

            if (salesPoint == null)
            {
                return NotFound();
            }

            return salesPoint;
        }

        // PUT: api/SalesPoints/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPoint(int id, SalesPoint salesPoint)
        {
            if (id != salesPoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPointExists(id))
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

        // POST: api/SalesPoints
        [HttpPost]
        public async Task<ActionResult<SalesPoint>> PostSalesPoint(SalesPoint salesPoint)
        {
          if (_context.SalesPoints == null)
          {
              return Problem("Entity set 'Context.SalesPoints'  is null.");
          }
            _context.SalesPoints.Add(salesPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesPoint", new { id = salesPoint.Id }, salesPoint);
        }

        // DELETE: api/SalesPoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesPoint(int id)
        {
            if (_context.SalesPoints == null)
            {
                return NotFound();
            }
            var salesPoint = await _context.SalesPoints.FindAsync(id);
            if (salesPoint == null)
            {
                return NotFound();
            }

            _context.SalesPoints.Remove(salesPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesPointExists(int id)
        {
            return (_context.SalesPoints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
