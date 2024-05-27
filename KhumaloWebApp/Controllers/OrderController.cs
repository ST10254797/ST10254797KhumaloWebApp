using KhumaloWebApp.Data;
using KhumaloWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace KhumaloWebApp.Controllers
{
    [Authorize(Roles = "Admin,Sales")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        
        public IActionResult OrderForm()
        {
            var order = new Order(); // Create a new instance of Order
            return View(order); // Pass the order instance to the view
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                // Handle invalid quantity
                ModelState.AddModelError("Quantity", "Quantity must be at least 1.");
                return View("OrderForm");
            }

            var product = await _context.ProductsDetails.FindAsync(productId);
            if (product == null)
            {
                // Handle product not found
                ModelState.AddModelError("ProductId", "Product not found.");
                return View("OrderForm");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Handle user not found
                return NotFound("User not found.");
            }

            var order = new Order
            {
                ProductId = productId,
                UserId = userId,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Product)
                .ToList();

            return View(orders);
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }


    }
}
