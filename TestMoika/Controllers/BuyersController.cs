﻿using System;
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
    public class BuyersController : ControllerBase
    {
        private readonly Context _context;

        public BuyersController(Context context)
        {
            _context = context;
        }

        // GET: api/Buyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        {
          if (_context.Buyers == null)
          {
              return NotFound();
          }
            return await _context.Buyers.ToListAsync();
        }

        // GET: api/Buyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
          if (_context.Buyers == null)
          {
              return NotFound();
          }
            var buyer = await _context.Buyers.FindAsync(id);

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/Buyers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return BadRequest();
            }

            _context.Entry(buyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerExists(id))
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

        // POST: api/Buyers
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
        {
          if (_context.Buyers == null)
          {
              return Problem("Entity set 'Context.Buyers'  is null.");
          }
            _context.Buyers.Add(buyer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            if (_context.Buyers == null)
            {
                return NotFound();
            }
            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }

            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuyerExists(int id)
        {
            return (_context.Buyers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
