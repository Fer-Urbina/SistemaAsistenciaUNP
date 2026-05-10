namespace SistemaAsistenciaUNP.Models
{
    public class Matricula
    {
        public int MatriculaId { get; set; }

        public int EstudianteId { get; set; }

        public Estudiante? Estudiante { get; set; }

        public int GrupoId { get; set; }

        public Grupo? Grupo { get; set; }

        public DateTime FechaMatricula { get; set; } = DateTime.Now;

        public bool Estado { get; set; } = true;
    }
}