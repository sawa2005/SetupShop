using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetupShop.Areas.Identity.Data;
using SetupShop.Data;
using SetupShop.Models;
using SQLitePCL;
using System.Security.Claims;

namespace SetupShop.Controllers
{
    public class MySetupsController : Controller
    {
        private readonly SetupShopContext _context;
        private readonly UserManager<SetupShopUser> _userManager;

        public MySetupsController(SetupShopContext context, IWebHostEnvironment webHostEnvironment, UserManager<SetupShopUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var setups = _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UserSetups)
                    .ThenInclude(us => us.Setup)
                .SelectMany(u => u.UserSetups)
                .Select(us => us.Setup)
                .ToList();

            return View(setups);
        }
    }
}
