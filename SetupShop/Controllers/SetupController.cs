using Microsoft.AspNetCore.Mvc;
using SetupShop.Data;

namespace SetupShop.Controllers
{
    public class SetupController : Controller
    {
        private readonly SetupShopContext _context;

        public SetupController(SetupShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
