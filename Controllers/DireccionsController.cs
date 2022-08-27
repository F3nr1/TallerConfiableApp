using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerMecanicoCApp.Models;

namespace TallerMecanicoCApp.Controllers
{
    public class DireccionsController : Controller
    {
        private readonly DBTallerMContext _context;

        public DireccionsController(DBTallerMContext context)
        {
            _context = context;
        }

        // GET: Direccions
        public async Task<IActionResult> Index()
        {
              return _context.Direccions != null ? 
                          View(await _context.Direccions.ToListAsync()) :
                          Problem("Entity set 'DBTallerMContext.Direccions'  is null.");
        }

        // GET: Direccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Direccions == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccions
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // GET: Direccions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Direccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DireccionId,Zona,TipoCalle,Num1,Num2,Num3")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direccion);
        }

        // GET: Direccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Direccions == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccions.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            return View(direccion);
        }

        // POST: Direccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DireccionId,Zona,TipoCalle,Num1,Num2,Num3")] Direccion direccion)
        {
            if (id != direccion.DireccionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.DireccionId))
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
            return View(direccion);
        }

        // GET: Direccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Direccions == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccions
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Direccions == null)
            {
                return Problem("Entity set 'DBTallerMContext.Direccions'  is null.");
            }
            var direccion = await _context.Direccions.FindAsync(id);
            if (direccion != null)
            {
                _context.Direccions.Remove(direccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
          return (_context.Direccions?.Any(e => e.DireccionId == id)).GetValueOrDefault();
        }
    }
}
