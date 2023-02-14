using Microsoft.AspNetCore.Mvc;
using SetupShop.Data;
using SetupShop.Models;
using SetupShop.Models.ViewModels;
using System.Net.Http.Headers;

namespace SetupShop.Controllers
{
    public class CartController : Controller
    {
        private readonly SetupContext _context;

        public CartController(SetupContext context)
        {
            _context = context;
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
            Setup product = await _context.Setup.FindAsync(id);

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

            TempData["Success"] = "The product has been added!";

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

            TempData["Success"] = "The product has been added!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
