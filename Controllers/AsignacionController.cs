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
    public class AsignacionController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public AsignacionController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: Asignacion
        public async Task<IActionResult> Index()
        {
              return _context.Calificacion != null ? 
                          View(await _context.Calificacion.ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
        }

        // GET: Asignacion/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .FirstOrDefaultAsync(m => m.CodigoAsignatura == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Asignacion/Create
        public IActionResult Create()
        {
            return View("Index");
        }

        // POST: Asignacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        // GET: Asignacion/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            return View(calificacion);
        }

        // POST: Asignacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (id != calificacion.CodigoAsignatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.CodigoAsignatura))
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
            return View(calificacion);
        }

        // GET: Asignacion/Delete/5
        public async Task<IActionResult> Delete(string? CodigoAsignatura, int Matricula)
        {
            if (CodigoAsignatura == null || Matricula == null||  _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .FirstOrDefaultAsync(m => m.CodigoAsignatura == CodigoAsignatura && m.Matricula == Matricula);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Asignacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? CodigoAsignatura, int Matricula)
        {
            if (_context.Calificacion == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
            }
            var calificacion = await _context.Calificacion.FindAsync(CodigoAsignatura, Matricula);
            if (calificacion != null)
            {
                _context.Calificacion.Remove(calificacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(string id)
        {
          return (_context.Calificacion?.Any(e => e.CodigoAsignatura == id)).GetValueOrDefault();
        }
    }
}
