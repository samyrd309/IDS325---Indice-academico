﻿using System;
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
    public class PublicacionController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;
        private readonly IConfiguration _config;

        public PublicacionController(IDS325___Indice_academicoContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        // GET: Publicacion
        public ActionResult Index(string CodigoAsignatura, int Seccion)
        {
            CodigoAsignatura = "IDS325";
            Seccion = 0;
            // Modificar Planchado

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string query = $"EXEC ListadoSeccion '{CodigoAsignatura}', {Seccion}";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                    }
                }
            }

            return View(ds);
        }
    

        // GET: Publicacion/Details/5
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

        // GET: Publicacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publicacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion,Trimestre")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
        }

        // GET: Publicacion/Edit/5
        public async Task<IActionResult> Edit(string CodigoAsignatura, int Matricula, string Trimestre)
        {
            if (CodigoAsignatura == null || Matricula == null || Trimestre == null || _context.Calificacion == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(CodigoAsignatura, Matricula, Trimestre);
            if (calificacion == null)
            {
                return NotFound();
            }
            return View(calificacion);
        }

        // POST: Publicacion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string CodigoAsignatura, int Matricula, string Trimestre, string Nota, [Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion,Trimestre")] Calificacion calificacion)
        {

            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string query = $"EXEC AsignarCalificación {Matricula}, '{Nota}', '{CodigoAsignatura}', '{Trimestre}'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                }
            }

            return View("Index");
        }

        // GET: Publicacion/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: Publicacion/Delete/5
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
