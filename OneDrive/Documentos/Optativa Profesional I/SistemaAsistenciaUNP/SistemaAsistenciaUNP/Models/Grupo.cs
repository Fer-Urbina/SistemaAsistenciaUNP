namespace SistemaAsistenciaUNP.Models
{
    public class Grupo
    {
        public int GrupoId { get; set; }

        public string NombreGrupo { get; set; } = string.Empty;

        public string? Turno { get; set; }

        public int? AulaId { get; set; }
        public Aula? Aula { get; set; }

        public int? PeriodoAcademicoId { get; set; }
        public PeriodoAcademico? PeriodoAcademico { get; set; }

        public int? HorarioId { get; set; }

        public Horario? Horario { get; set; }

        public int AsignaturaId { get; set; }

        public Asignatura? Asignatura { get; set; }

        public int DocenteId { get; set; }

        public Docente? Docente { get; set; }

        public bool Estado { get; set; } = true;
    }
}