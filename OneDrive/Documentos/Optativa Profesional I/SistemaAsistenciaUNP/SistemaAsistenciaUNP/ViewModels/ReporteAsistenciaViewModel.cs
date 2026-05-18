namespace SistemaAsistenciaUNP.ViewModels
{
    public class ReporteAsistenciaViewModel
    {
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Asignatura { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;
        public string Docente { get; set; } = string.Empty;
        public string Aula { get; set; } = string.Empty;
        public string EstadoAsistencia { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}