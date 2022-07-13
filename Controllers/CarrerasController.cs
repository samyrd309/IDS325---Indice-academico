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
    public class CarrerasController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public CarrerasController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
              return _context.Carrera != null ? 
                          View(await _context.Carrera.ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Carrera'  is null.");
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Carrera == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera
                .FirstOrDefaultAsync(m => m.CodigoCarrera == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCarrera,NombreCarrera,CodigoArea")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Carrera == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }
            return View(carrera);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoCarrera,NombreCarrera,CodigoArea")] Carrera carrera)
        {
            if (id != carrera.CodigoCarrera)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.CodigoCarrera))
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
            return View(carrera);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Carrera == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carrera
                .FirstOrDefaultAsync(m => m.CodigoCarrera == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Carrera == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Carrera'  is null.");
            }
            var carrera = await _context.Carrera.FindAsync(id);
            if (carrera != null)
            {
                _context.Carrera.Remove(carrera);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(string id)
        {
          return (_context.Carrera?.Any(e => e.CodigoCarrera == id)).GetValueOrDefault();
        }
    }
}
