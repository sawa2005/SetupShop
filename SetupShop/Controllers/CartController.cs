using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetupShop.Areas.Identity.Data;
using SetupShop.Data;
using SetupShop.Models;
using SetupShop.Models.ViewModels;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SetupShop.Controllers
{
    public class CartController : Controller
    {
        private readonly SetupShopContext _context;
        private readonly UserManager<SetupShopUser> _userManager;

        public CartController(SetupShopContext context, UserManager<SetupShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new() 
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };


            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            Setup product = await _context.Setups.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

                CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

                if (cartItem == null)
                {
                    cart.Add(new CartItem(product));
                }
                else
                {
                    cartItem.Quantity += 1;
                }

                HttpContext.Session.SetJson("Cart", cart);

                TempData["Success"] = "The product has been added!";

                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product quantity has been reduced!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            var user = _context.Users.Single(u => u.Id == userId);

            foreach (var item in cartVM.CartItems)
            {
                var setup = _context.Setups
                    .Include(s => s.UserSetups)
                    .Single(s => s.Id == item.ProductId);

                setup.UserSetups.Add(new UserSetup
                {
                    User = user,
                    Setup = setup
                });

                _context.SaveChanges();
            }

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
