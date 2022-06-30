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
    public class AsignaturasController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public AsignaturasController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: Asignaturas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Asignatura.Where(a => a.VigenciaAsignatura == true).ToListAsync());
        }

        // GET: Asignaturas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Asignatura == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignatura
                .FirstOrDefaultAsync(m => m.CodigoAsignatura == id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }

        // GET: Asignaturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asignaturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoAsignatura,CodigoCarrera,CodigoArea,Credito,NombreAsignatura")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asignatura);
        }

        // GET: Asignaturas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Asignatura == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignatura.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }
            return View(asignatura);
        }

        // POST: Asignaturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoAsignatura,CodigoCarrera,CodigoArea,Credito,NombreAsignatura")] Asignatura asignatura)
        {
            if (id != asignatura.CodigoAsignatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    asignatura.VigenciaAsignatura = true;
                    _context.Update(asignatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaExists(asignatura.CodigoAsignatura))
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
            return View(asignatura);
        }

        // GET: Asignaturas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Asignatura == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignatura
                .FirstOrDefaultAsync(m => m.CodigoAsignatura == id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }

        // POST: Asignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Asignatura == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Asignatura'  is null.");
            }
            var asignatura = await _context.Asignatura.FindAsync(id);
            asignatura.VigenciaAsignatura = false;
            if (asignatura != null)
            {
                _context.Asignatura.Update(asignatura); 
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool AsignaturaExists(string id)
        {
          return _context.Asignatura.Any(e => e.CodigoAsignatura == id);
        }
    }
}
