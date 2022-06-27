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
    public class SeccionsController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public SeccionsController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: Seccions
        public async Task<IActionResult> Index()
        {
              return _context.Seccion != null ? 
                          View(await _context.Seccion.Where(s => s.VigenciaSeccion == true).ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Seccion'  is null.");
        }

        // GET: Seccions/Details/5
        public async Task<IActionResult> Details(int? IdSeccion, string CodigoAsignatura)
        {
            if (IdSeccion == null || CodigoAsignatura == null ||_context.Seccion == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion
                .FirstOrDefaultAsync(m => m.IdSeccion == IdSeccion && m.CodigoAsignatura == CodigoAsignatura);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // GET: Seccions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeccion,Matricula,CodigoAsignatura")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seccion);
        }

        // GET: Seccions/Edit/5
        public async Task<IActionResult> Edit(int? IdSeccion, string CodigoAsignatura)
        {
            if (IdSeccion == null || CodigoAsignatura == null || _context.Seccion == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion
                .FirstOrDefaultAsync(m => m.IdSeccion == IdSeccion && m.CodigoAsignatura == CodigoAsignatura);
            if (seccion == null)
            {
                return NotFound();
            }
            return View(seccion);
        }

        // POST: Seccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdSeccion, string CodigoAsignatura, [Bind("IdSeccion,Matricula,CodigoAsignatura")] Seccion seccion)
        {
            if (IdSeccion != seccion.IdSeccion || CodigoAsignatura != seccion.CodigoAsignatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeccionExists(seccion.IdSeccion))
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
            return View(seccion);
        }

        // GET: Seccions/Delete/5
        public async Task<IActionResult> Delete(int? IdSeccion, string CodigoAsignatura)
        {
            if (IdSeccion == null || CodigoAsignatura == null || _context.Seccion == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccion
                .FirstOrDefaultAsync(m => m.IdSeccion == IdSeccion && m.CodigoAsignatura == CodigoAsignatura);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // POST: Seccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdSeccion, string CodigoAsignatura)
        {
            if (_context.Seccion == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Seccion'  is null.");
            }
            var seccion = await _context.Seccion.FindAsync(IdSeccion, CodigoAsignatura);
            seccion.VigenciaSeccion = false;
            if (seccion != null)
            {
                _context.Seccion.Update(seccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeccionExists(int id)
        {
          return (_context.Seccion?.Any(e => e.IdSeccion == id)).GetValueOrDefault();
        }
    }
}
