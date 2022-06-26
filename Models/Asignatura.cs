using System.ComponentModel.DataAnnotations;

namespace IDS325___Indice_academico.Models
{
    public class Asignatura
    {
        [Key]
        public string CodigoAsignatura { get; set; }
        public string CodigoCarrera { get; set; }
        public string CodigoArea { get; set; }
        public int Credito { get; set; }
        public string NombreAsignatura { get; set; }
        
    }
}
