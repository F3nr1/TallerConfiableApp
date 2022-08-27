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
    public class MecanicoesController : Controller
    {
        private readonly DBTallerMContext _context;

        public MecanicoesController(DBTallerMContext context)
        {
            _context = context;
        }

        // GET: Mecanicoes
        public async Task<IActionResult> Index()
        {
            var dBTallerMContext = _context.Mecanicos.Include(m => m.Persona);
            return View(await dBTallerMContext.ToListAsync());
        }

        // GET: Mecanicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos
                .Include(m => m.Persona)
                .FirstOrDefaultAsync(m => m.MecanicoId == id);
            if (mecanico == null)
            {
                return NotFound();
            }

            return View(mecanico);
        }

        // GET: Mecanicoes/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "PersonaId");
            return View();
        }

        // POST: Mecanicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MecanicoId,NivelEstudios,PersonaId")] Mecanico mecanico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mecanico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "PersonaId", mecanico.PersonaId);
            return View(mecanico);
        }

        // GET: Mecanicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos.FindAsync(id);
            if (mecanico == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "PersonaId", mecanico.PersonaId);
            return View(mecanico);
        }

        // POST: Mecanicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MecanicoId,NivelEstudios,PersonaId")] Mecanico mecanico)
        {
            if (id != mecanico.MecanicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mecanico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MecanicoExists(mecanico.MecanicoId))
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
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "PersonaId", mecanico.PersonaId);
            return View(mecanico);
        }

        // GET: Mecanicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos
                .Include(m => m.Persona)
                .FirstOrDefaultAsync(m => m.MecanicoId == id);
            if (mecanico == null)
            {
                return NotFound();
            }

            return View(mecanico);
        }

        // POST: Mecanicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mecanicos == null)
            {
                return Problem("Entity set 'DBTallerMContext.Mecanicos'  is null.");
            }
            var mecanico = await _context.Mecanicos.FindAsync(id);
            if (mecanico != null)
            {
                _context.Mecanicos.Remove(mecanico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MecanicoExists(int id)
        {
          return (_context.Mecanicos?.Any(e => e.MecanicoId == id)).GetValueOrDefault();
        }
    }
}
