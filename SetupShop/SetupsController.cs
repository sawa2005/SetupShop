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

        public SetupsController(SetupContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("Id,Name,Author,File,Car,Track,Season,Week,Series,VideoUrl")] Setup setup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,File,Car,Track,Season,Week,Series,VideoUrl")] Setup setup)
        {
            if (id != setup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetupExists(setup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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

            return View(setup);
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
