using System.ComponentModel.DataAnnotations;
namespace IDS325___Indice_academico.Models
{
    public class Calificacion
    {
   
        public int Matricula { get; set; }
        public string CodigoAsignatura { get; set; }
        public string NombreAsignatura { get; set; }
        public string Nota { get; set; }
        public int Credito { get; set; }
    }
}
