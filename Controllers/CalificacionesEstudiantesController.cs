using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using IDS325___Indice_academico.Models;
using IDS325___Indice_academico.Data;
using Microsoft.AspNetCore.Authorization;

namespace IDS325___Indice_academico.Controllers
{
    [Authorize]
    public class CalificacionesEstudiantesController : Controller
    {
        private readonly IDS325___Indice_academicoContext _context;
        private readonly IConfiguration _config;

        public CalificacionesEstudiantesController(IDS325___Indice_academicoContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        
        public IActionResult Index(Persona _persona)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string query = $"EXEC MostrarCalificaciones '{_persona.Matricula.ToString()}'";
                using (SqlCommand cmd = new SqlCommand(query,conn))
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
    }
}
