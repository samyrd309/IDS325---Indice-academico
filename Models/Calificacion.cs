using System.Data.SqlClient;
using System.Data;

namespace IDS325___Indice_academico.Models
{
    public class Calificacion
    {
        private readonly IConfiguration _config;

        public int Matricula { get; set; }
        public string CodigoAsignatura { get; set; }
        public string? Nota { get; set; }
        public int IdSeccion { get; set; }
        public bool? VigenciaCalificacion { get; set; }
        public string Trimestre { get; set; }



        public Calificacion()
        {

        }
    }
}
