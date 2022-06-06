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
    public class SalesController : ControllerBase
    {
        private readonly Context _context;

        public SalesController(Context context)
        {
            _context = context;
        }

        // POST: api/Sales/
        [HttpPost("{productId}")]
        public async Task<ActionResult<Sale>> CreateSale(int? buyerId,  int productId, int countBuyProduct = 0)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'Context.Sales'  is null.");
            }

            if (countBuyProduct <= 0)
            {
                return BadRequest("countBuyProduct less than zero");
            }

            Buyer? buyer = null;
            if (buyerId.HasValue)
            {
                buyer = await _context.Buyers.FindAsync(buyerId.Value);
                if (buyer == null)
                {
                    return BadRequest("Buyer not found");
                }
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return BadRequest("Product not found");
            }

            //Получили сущность с id продуктом и максимальным количеством доступных к продаже
            var providedProduct = await _context.ProvidedProducts.Where(x => x.ProductId == productId).OrderByDescending(x => x.ProductQuantity).FirstOrDefaultAsync();
            if (providedProduct == null)
            {
                return BadRequest("ProvidedProduct not found");
            }

            if (providedProduct.ProductQuantity < countBuyProduct)
            {
                return BadRequest("countBuyProduct more than the available quantity of the product");
            }

            //Получаем торговую точку по найденному продукту
            var salePoint = await _context.SalesPoints.Where(x => x.ProvidedProducts.Any(y => y.Id == providedProduct.Id)).FirstOrDefaultAsync();
            if (salePoint == null)
            {
                return BadRequest("SalesPoint not found");
            }

            //Изменяем кол-во доступных товораво у точки
            providedProduct.ProductQuantity -= countBuyProduct;


            Sale sale = new Sale
            {
                Date = DateTime.Now,
                Time = DateTime.Now.TimeOfDay,
                SalesPointId = salePoint.Id,
            };

            if (buyerId.HasValue)
            {
                sale.BuyerId = buyer.Id;
            }


            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            SalesData salesData = new SalesData
            {
                ProductId = productId,
                ProductQuantity = countBuyProduct,
                PropductIdAmount = product.Price * countBuyProduct,
                SaleId = sale.Id,
            };

            _context.SalesDates.Add(salesData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale); ;
        }


        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            return await _context.Sales.ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
          if (_context.Sales == null)
          {
              return Problem("Entity set 'Context.Sales'  is null.");
          }
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
