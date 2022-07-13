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
    public class DocentesController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public DocentesController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        // GET: Docentes
        public async Task<IActionResult> Index()
        {
              return _context.Persona != null ? 
                          View(await _context.Persona.Where((d => d.IdRol.Equals(3) && d.VigenciaPersona == true)).ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Persona'  is null.");
        }

        // GET: Docentes/Details/5
        public async Task<IActionResult> Details(int? Matricula)
        {
            if (Matricula == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.Matricula == Matricula);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Docentes/Create
        public IActionResult Create()
        {
            ViewBag.AreaList = _context.AreaAcademica.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _context.Carrera.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();
            return View();
        }

        // POST: Docentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,Contraseña,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,Indice,VigenciaPersona")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Docentes/Edit/5
        public async Task<IActionResult> Edit(int? Matricula, string Contraseña)
        {
            if (Matricula == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(Matricula, Contraseña);
            if (persona == null)
            {
                return NotFound();
            }

            ViewBag.AreaList = _context.AreaAcademica.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _context.Carrera.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();
            return View(persona);
        }

        // POST: Docentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Matricula, string Contraseña, [Bind("Matricula,Contraseña,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,Indice,VigenciaPersona")] Persona persona)
        {
            if (Matricula != persona.Matricula || Contraseña != persona.Contraseña)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    persona.VigenciaPersona = true;
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists((int)persona.Matricula))
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
            return View(persona);
        }

        // GET: Docentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.Matricula == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Docentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Persona == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Persona'  is null.");
            }
            var persona = await _context.Persona.FindAsync(id);
            if (persona != null)
            {
                _context.Persona.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return (_context.Persona?.Any(e => e.Matricula == id)).GetValueOrDefault();
        }
    }
}
