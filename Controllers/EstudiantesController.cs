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
    public class EstudiantesController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;

        public EstudiantesController(IDS325___Indice_academicoContext context)
        {
            _context = context;
        }

        public static Persona estudiante = new Persona();

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            estudiante = await _context.Persona.FirstOrDefaultAsync(m => m.IdRol == 2);   
            return _context.Persona != null ? 
                          View("Index", await _context.Persona.Where(e => e.IdRol.Equals(2) && e.VigenciaPersona == true).ToListAsync()) : // Regular para que filtre por rol de estudiante
                          Problem("Entity set 'IDS325___Indice_academicoContext.Persona'  is null.");
        }


        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,Contraseña,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,Indice")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matricula,Contraseña,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,Indice")] Persona persona)
        {
            if (id != persona.Matricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Matricula))
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

        // GET: Estudiantes/Delete/5
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

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Persona == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Persona'  is null.");
            }
            var persona = await _context.Persona.FindAsync(id);
            persona.VigenciaPersona = false;
            if (persona != null)
            {
                _context.Persona.Update(persona);
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
