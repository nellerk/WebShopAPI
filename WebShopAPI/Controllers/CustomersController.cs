using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Context;
using WebShopAPI.Entities;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly WebshopContext _context;

        public CustomersController(WebshopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCustomers() => Ok(_context.Customers.ToList());

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Address = updatedCustomer.Address;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
