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
    public class DiagnosticoesController : Controller
    {
        private readonly DBTallerMContext _context;

        public DiagnosticoesController(DBTallerMContext context)
        {
            _context = context;
        }

        // GET: Diagnosticoes
        public async Task<IActionResult> Index()
        {
            var dBTallerMContext = _context.Diagnosticos.Include(d => d.Vehiculo);
            return View(await dBTallerMContext.ToListAsync());
        }

        // GET: Diagnosticoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Vehiculo)
                .FirstOrDefaultAsync(m => m.DiagId == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticoes/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId");
            return View();
        }

        // POST: Diagnosticoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiagId,TipoRevision,RevisionNiveles,TipoRepuesto,Repuesto,VehiculoId")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", diagnostico.VehiculoId);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", diagnostico.VehiculoId);
            return View(diagnostico);
        }

        // POST: Diagnosticoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiagId,TipoRevision,RevisionNiveles,TipoRepuesto,Repuesto,VehiculoId")] Diagnostico diagnostico)
        {
            if (id != diagnostico.DiagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.DiagId))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", diagnostico.VehiculoId);
            return View(diagnostico);
        }

        // GET: Diagnosticoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnosticos == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Vehiculo)
                .FirstOrDefaultAsync(m => m.DiagId == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnosticos == null)
            {
                return Problem("Entity set 'DBTallerMContext.Diagnosticos'  is null.");
            }
            var diagnostico = await _context.Diagnosticos.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnosticos.Remove(diagnostico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
          return (_context.Diagnosticos?.Any(e => e.DiagId == id)).GetValueOrDefault();
        }
    }
}
