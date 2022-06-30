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
using Microsoft.Data.SqlClient;

namespace IDS325___Indice_academico.Controllers
{
    public class PublicacionController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;
        private readonly IConfiguration _config;
        private readonly IEnumerable<object> tblEstudiantes;

        public PublicacionController(IDS325___Indice_academicoContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Publicacion
        public ActionResult Index(string CodigoAsignatura, int Seccion)
        {
            CodigoAsignatura = "IDS325";
            Seccion = 1;
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


        public ActionResult test(string CodigoAsignatura, int Seccion)
        {
            CodigoAsignatura = "IDS325";
            Seccion = 1;
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
        public async Task<IActionResult> Create([Bind("Matricula,CodigoAsignatura,Nota,IdSeccion,VigenciaCalificacion")] Calificacion calificacion)
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
        public ActionResult Publicar(string? CodigoAsignatura, int Matricula, string? txtNota, string Trimestre)
        {
            Trimestre = "2022-2 ";
            
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string query = $"EXEC AsignarCalificación {Matricula}, '{txtNota}', '{CodigoAsignatura}', '{Trimestre}'  ";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return View("test");
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}
