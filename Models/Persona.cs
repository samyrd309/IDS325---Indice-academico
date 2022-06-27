using System.ComponentModel.DataAnnotations;

namespace IDS325___Indice_academico.Models
{
    public class Persona
    {
        [Key]
        [Required]
        public int Matricula { get; set; }
        [Required]
        public string Contraseña { get; set; }
        public int IdRol { get; set; }
        public string? Carrera { get; set; }
        public string? CodigoArea { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public decimal? Indice { get; set; }

    }
}
