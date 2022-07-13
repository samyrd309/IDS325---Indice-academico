using System.ComponentModel.DataAnnotations;

namespace IDS325___Indice_academico.Models
{
    public class AreaAcademica
    {
        [Key]
        public string CodigoArea { get; set; }
        public string NombreArea { get; set; }
    }
}
