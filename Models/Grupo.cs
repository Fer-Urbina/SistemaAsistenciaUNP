namespace SistemaAsistenciaUNP.Models
{
    public class Grupo
    {
        public int GrupoId { get; set; }

        public string NombreGrupo { get; set; } = string.Empty;

        public string? Turno { get; set; }

        public string? Aula { get; set; }

        public string? PeriodoAcademico { get; set; }

        public int AsignaturaId { get; set; }

        public Asignatura? Asignatura { get; set; }

        public int DocenteId { get; set; }

        public Docente? Docente { get; set; }

        public bool Estado { get; set; } = true;
    }
}