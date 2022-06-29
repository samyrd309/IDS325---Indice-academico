﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using IDS325___Indice_academico.Models;
using IDS325___Indice_academico.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

        
        public IActionResult Index(int Matricula)
        {
            DataSet data = new DataSet();
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string q = $"CalcularIndice '{Matricula}'";
                using (SqlCommand sql = new SqlCommand(q))
                {
                    sql.Connection = con;
                    sql.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        ViewBag.Indice = reader["Indice"].ToString();
                        ViewBag.Total = reader["TotalCreditos"].ToString();
                        ViewBag.Merito = reader["Meritos"].ToString();
                    }

                    con.Close();
                }
            }

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("IDS325___Indice_academicoContext")))
            {
                string query = $"EXEC MostrarCalificaciones '{Matricula}'";
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

        public async Task<IActionResult> Ranking(string txtCarrera)
        {
            return _context.Persona != null ?
                    View(await _context.Persona.Where(p => p.VigenciaPersona == true && p.IdRol == 2 && p.Carrera == txtCarrera).OrderBy(p => p.Indice).ToListAsync()) :
                    Problem("Entity set 'IDS325___Indice_academicoContext.Calificacion'  is null.");
        }
    }
}
