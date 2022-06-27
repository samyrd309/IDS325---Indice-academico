using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325___Indice_academico.Data;
using IDS325___Indice_academico.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json;

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
                          View(await _context.Calificacion.Where(c => c.VigenciaCalificacion == true).ToListAsync()) :
                          Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string txtMatricula, string txtAsignatura, string txtSeccion)
        {
            int matricula = Convert.ToInt16(txtMatricula);
            int seccion = Convert.ToInt16(txtSeccion);
            string asignatura = txtAsignatura;

            Calificacion calificacion = new Calificacion();
            calificacion.Matricula = matricula;
            calificacion.CodigoAsignatura = asignatura;
            calificacion.IdSeccion = seccion;

            _context.Add(calificacion);
            _context.SaveChanges();

            return View();
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
            return View();
        }

        // POST: Asignacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,CodigoAsignatura,Nota,IdSeccion")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calificacion);
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
        public async Task<IActionResult> Edit(string id, [Bind("Matricula,CodigoAsignatura,Nota,IdSeccion")] Calificacion calificacion)
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

        // POST: Asignacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Calificacion == null)
            {
                return Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
            }
            var calificacion = await _context.Calificacion.FindAsync(id);
            calificacion.VigenciaCalificacion = false;
            if (calificacion != null)
            {
                _context.Calificacion.Update(calificacion);
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

/*private readonly IConfiguration _config;
        private readonly HospitalContext _context;

        public CuentasController(IConfiguration configuration, HospitalContext context)
        {
            _config = configuration;
            _context = context;
        }

        public static Cuenta cuenta = new Cuenta();

        public IActionResult Index(string IdPersona)
        {
            DataSet data = new DataSet();
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("HospitalContext")))
            {
                string q = $"GetTotalCuenta '{IdPersona}'";
                using (SqlCommand sql = new SqlCommand(q))
                {
                    sql.Connection = con;
                    sql.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        ViewBag.Total = reader["Cuenta_Balance"].ToString();
                    }
                    
                    con.Close();
                }
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("HospitalContext")))
            {
                string query = $"EXEC GetDetalleCuenta '{IdPersona}'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    using (SqlDataAdapter dsa = new SqlDataAdapter())
                    {
                        dsa.SelectCommand = cmd;
                        dsa.Fill(ds);
                    }
                }
            }
            return View(ds);
        }*/