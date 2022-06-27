using System.ComponentModel.DataAnnotations;

namespace IDS325___Indice_academico.Models
{
    public class Persona
    {
        public int Matricula { get; set; }
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        public string? Carrera { get; set; }
        public string? CodigoArea { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public decimal? Indice { get; set; }
        public bool VigenciaPersona { get; set; }
    }
}
