using Microsoft.AspNetCore.Mvc;
using SetupShop.Data;

namespace SetupShop.Controllers
{
    public class SetupController : Controller
    {
        private readonly SetupContext _context;

        public SetupController(SetupContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
