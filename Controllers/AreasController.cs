using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325___Indice_academico.Data;
using IDS325___Indice_academico.Models;

namespace IDS325___Indice_academico.Controllers
{
    public class AreaAcademicasController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public AreaAcademicasController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: AreaAcademicas
        public async Task<IActionResult> Index()
        {
              return _context.AreaAcademica != null ? 
                          View(await _context.AreaAcademica.ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.AreaAcademica'  is null.");
        }

        // GET: AreaAcademicas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AreaAcademica == null)
            {
                return NotFound();
            }

            var AreaAcademica = await _context.AreaAcademica
                .FirstOrDefaultAsync(m => m.CodigoArea == id);
            if (AreaAcademica == null)
            {
                return NotFound();
            }

            return View(AreaAcademica);
        }

        // GET: AreaAcademicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AreaAcademicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoAreaAcademica,NombreAreaAcademica")] AreaAcademica AreaAcademica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(AreaAcademica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(AreaAcademica);
        }

        // GET: AreaAcademicas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AreaAcademica == null)
            {
                return NotFound();
            }

            var AreaAcademica = await _context.AreaAcademica.FindAsync(id);
            if (AreaAcademica == null)
            {
                return NotFound();
            }
            return View(AreaAcademica);
        }

        // POST: AreaAcademicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoAreaAcademica,NombreAreaAcademica")] AreaAcademica AreaAcademica)
        {
            if (id != AreaAcademica.CodigoArea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(AreaAcademica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaAcademicaExists(AreaAcademica.CodigoArea))
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
            return View(AreaAcademica);
        }

        // GET: AreaAcademicas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AreaAcademica == null)
            {
                return NotFound();
            }

            var AreaAcademica = await _context.AreaAcademica
                .FirstOrDefaultAsync(m => m.CodigoArea == id);
            if (AreaAcademica == null)
            {
                return NotFound();
            }

            return View(AreaAcademica);
        }

        // POST: AreaAcademicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AreaAcademica == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.AreaAcademica'  is null.");
            }
            var AreaAcademica = await _context.AreaAcademica.FindAsync(id);
            if (AreaAcademica != null)
            {
                _context.AreaAcademica.Remove(AreaAcademica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaAcademicaExists(string id)
        {
          return (_context.AreaAcademica?.Any(e => e.CodigoArea == id)).GetValueOrDefault();
        }
    }
}
