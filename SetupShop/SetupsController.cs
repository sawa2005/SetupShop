using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SetupShop.Areas.Identity.Data;
using SetupShop.Data;
using SetupShop.Models;

namespace SetupShop
{
    [Authorize(Policy = "RequireAuthor")]
    public class SetupsController : Controller
    {
        private readonly SetupShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<SetupShopUser> _userManager;

        public SetupsController(SetupShopContext context, IWebHostEnvironment webHostEnvironment, UserManager<SetupShopUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: Setups
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.Setups != null ? 
                          View(await _context.Setups.ToListAsync()) :
                          Problem("Entity set 'SetupContext.Setup'  is null.");
        }

        public async Task<IActionResult> Author()
        {
            return _context.Setups != null ?
                          View(await _context.Setups.ToListAsync()) :
                          Problem("Entity set 'SetupContext.Setup'  is null.");
        }

        // GET: Setups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Setups == null)
            {
                return NotFound();
            }

            var setup = await _context.Setups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (setup == null)
            {
                return NotFound();
            }

            return View(setup);
        }

        // GET: Setups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Setups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setup setup)
        {
            if (ModelState.IsValid)
            {
                if (setup.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media");
                    string imageName = Guid.NewGuid().ToString() + "_" + setup.ImageUpload.FileName;

                    imageName = imageName.Replace(" ", "_");

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await setup.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    setup.Image = imageName;
                }

                if (setup.FileUpload != null)
                {             
                    using (var memoryStream = new MemoryStream())
                    {
                        var file = setup.FileUpload;

                        await file.CopyToAsync(memoryStream);

                        setup.FileName = file.FileName;
                        setup.FileType = file.ContentType;
                        setup.File = memoryStream.ToArray();
                    }
                }

                _context.Add(setup);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The setup has been added!";

                return RedirectToAction("Author");
            }

            return View(setup);
        }

        // GET: Setups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Setups == null)
            {
                return NotFound();
            }

            var setup = await _context.Setups.FindAsync(id);
            if (setup == null)
            {
                return NotFound();
            }
            return View(setup);
        }

        // POST: Setups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Setup setup)
        {
            if (id != setup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (setup.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media");
                    string imageName = Guid.NewGuid().ToString() + "_" + setup.ImageUpload.FileName;

                    imageName = imageName.Replace(" ", "_");

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await setup.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    setup.Image = imageName;
                }

                if (setup.FileUpload != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var file = setup.FileUpload;

                        await file.CopyToAsync(memoryStream);

                        setup.FileName = file.FileName;
                        setup.FileType = file.ContentType;
                        setup.File = memoryStream.ToArray();
                    }
                }

                _context.Update(setup);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been edited!";
            }

            return View(setup);
        }

        // GET: Setups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Setups == null)
            {
                return NotFound();
            }

            var setup = await _context.Setups
                .FirstOrDefaultAsync(m => m.Id == id);

            if (setup == null)
            {
                return NotFound();
            }

            if (!string.Equals(setup.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media");
                string oldImagePath = Path.Combine(uploadsDir, setup.Image);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Setups.Remove(setup);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The product has been deleted!";

            return RedirectToAction("Author");
        }

        // POST: Setups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Setups == null)
            {
                return Problem("Entity set 'SetupContext.Setup'  is null.");
            }
            var setup = await _context.Setups.FindAsync(id);
            if (setup != null)
            {
                _context.Setups.Remove(setup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(int id)
        {
            using (var db = _context)
            {
                var fileUpload = await db.Setups.FindAsync(id);

                if (fileUpload != null)
                {
                    return File(fileUpload.File, fileUpload.FileType, fileUpload.FileName);
                }
            }

            return NotFound("File doesn't exist.");
        }

        private bool SetupExists(int id)
        {
          return (_context.Setups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
