namespace IDS325___Indice_academico.Models
{
    public class Calificacion
    {
        public int Matricula { get; set; }
        public string CodigoAsignatura { get; set; }
        public string Nota { get; set; }
        public int IdSeccion { get; set; }
        public bool VigenciaCalificacion { get; set; }

        public Calificacion()
        {

        }
    }
}
