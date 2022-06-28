using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325___Indice_academico.Data;
using IDS325___Indice_academico.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IDS325___Indice_academico.Controllers
{
    public class RevisionController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;
        private readonly IConfiguration _config;

        public RevisionController(IDS325___Indice_academicoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Revision
        public async Task<IActionResult> Index()
        {
              return _context.Calificacion != null ? 
                          View(await _context.Calificacion.ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
        }

        // GET: Revision/Details/5
        public async Task<IActionResult> Details(string? CodigoAsignatura, int Matricula)
        {
            if (CodigoAsignatura == null || Matricula == null || _context.Calificacion == null)
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

        // GET: Revision/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Revision/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                calificacion.VigenciaCalificacion = true;
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
        }

        // GET: Revision/Edit/5
        public async Task<IActionResult> Edit(string? CodigoAsignatura, int Matricula)
        {
            if (CodigoAsignatura == null || Matricula == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(CodigoAsignatura, Matricula);
            if (calificacion == null)
            {
                return NotFound();
            }
            return View(calificacion);
        }

        // POST: Revision/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? CodigoAsignatura, int Matricula, [Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (CodigoAsignatura != calificacion.CodigoAsignatura && Matricula != calificacion.Matricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                    // PROCESO SP ÜPDATE DEL NUEVO ÏNDICE

                    DataSet ds = new DataSet();
                    using (SqlConnection con = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
                    {
                        string query = $"EXEC ModificarIndice '{calificacion.Matricula}'";
                        using (SqlCommand sql = new SqlCommand(query))
                        {
                            sql.Connection = con;
                            sql.CommandType = CommandType.Text;
                            con.Open();
                            sql.ExecuteNonQuery();
                            con.Close();
                        }
                    }
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

        // GET: Revision/Delete/5
        public async Task<IActionResult> Delete(string? CodigoAsignatura, int Matricula)
        {
            if (CodigoAsignatura == null || Matricula == null || _context.Calificacion == null)
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

        // POST: Revision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Calificacion == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
            }
            var calificacion = await _context.Calificacion.FindAsync(id);
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
