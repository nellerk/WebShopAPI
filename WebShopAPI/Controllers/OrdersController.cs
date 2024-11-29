using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopAPI.Context;
using WebShopAPI.Entities;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WebshopContext _context;

        public OrdersController(WebshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
            => Ok(_context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .ToList());

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            order.TotalAmount = order.Products.Sum(p => p.Price);

            _context.Orders.Add(order);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            order.CustomerId = updatedOrder.CustomerId;
            order.Products = updatedOrder.Products;
            order.TotalAmount = updatedOrder.Products.Sum(p => p.Price);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
