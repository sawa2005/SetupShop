using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SetupShop.Data;
using SetupShop.Models;

namespace SetupShop
{
    public class SetupsController : Controller
    {
        private readonly SetupContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SetupsController(SetupContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Setups
        public async Task<IActionResult> Index()
        {
              return _context.Setup != null ? 
                          View(await _context.Setup.ToListAsync()) :
                          Problem("Entity set 'SetupContext.Setup'  is null.");
        }

        // GET: Setups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Setup == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup
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

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await setup.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    setup.Image = imageName;
                }

                _context.Add(setup);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The product has been added!";

                return RedirectToAction("Index");
            }

            return View(setup);
        }

        // GET: Setups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Setup == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup.FindAsync(id);
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

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await setup.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    setup.Image = imageName;
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
            if (id == null || _context.Setup == null)
            {
                return NotFound();
            }

            var setup = await _context.Setup
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

            _context.Setup.Remove(setup);
            await _context.SaveChangesAsync();

            TempData["Success"] = "The product has been deleted!";

            return RedirectToAction("Index");
        }

        // POST: Setups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Setup == null)
            {
                return Problem("Entity set 'SetupContext.Setup'  is null.");
            }
            var setup = await _context.Setup.FindAsync(id);
            if (setup != null)
            {
                _context.Setup.Remove(setup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetupExists(int id)
        {
          return (_context.Setup?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
