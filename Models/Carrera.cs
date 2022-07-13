using System.ComponentModel.DataAnnotations;

namespace IDS325___Indice_academico.Models
{
    public class Carrera
    {
        [Key]
        public string CodigoCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string CodigoArea { get; set; }
    }
}
