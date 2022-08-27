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
    public class SoatsController : Controller
    {
        private readonly DBTallerMContext _context;

        public SoatsController(DBTallerMContext context)
        {
            _context = context;
        }

        // GET: Soats
        public async Task<IActionResult> Index()
        {
            var dBTallerMContext = _context.Soats.Include(s => s.Vehiculo);
            return View(await dBTallerMContext.ToListAsync());
        }

        // GET: Soats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Soats == null)
            {
                return NotFound();
            }

            var soat = await _context.Soats
                .Include(s => s.Vehiculo)
                .FirstOrDefaultAsync(m => m.SoatId == id);
            if (soat == null)
            {
                return NotFound();
            }

            return View(soat);
        }

        // GET: Soats/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId");
            return View();
        }

        // POST: Soats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoatId,FechaVencimiento,PuedeTransitar,VehiculoId")] Soat soat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", soat.VehiculoId);
            return View(soat);
        }

        // GET: Soats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Soats == null)
            {
                return NotFound();
            }

            var soat = await _context.Soats.FindAsync(id);
            if (soat == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", soat.VehiculoId);
            return View(soat);
        }

        // POST: Soats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SoatId,FechaVencimiento,PuedeTransitar,VehiculoId")] Soat soat)
        {
            if (id != soat.SoatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoatExists(soat.SoatId))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", soat.VehiculoId);
            return View(soat);
        }

        // GET: Soats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Soats == null)
            {
                return NotFound();
            }

            var soat = await _context.Soats
                .Include(s => s.Vehiculo)
                .FirstOrDefaultAsync(m => m.SoatId == id);
            if (soat == null)
            {
                return NotFound();
            }

            return View(soat);
        }

        // POST: Soats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Soats == null)
            {
                return Problem("Entity set 'DBTallerMContext.Soats'  is null.");
            }
            var soat = await _context.Soats.FindAsync(id);
            if (soat != null)
            {
                _context.Soats.Remove(soat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoatExists(int id)
        {
          return (_context.Soats?.Any(e => e.SoatId == id)).GetValueOrDefault();
        }
    }
}
