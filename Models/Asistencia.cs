namespace SistemaAsistenciaUNP.Models
{
    public class Asistencia
    {
        public int AsistenciaId { get; set; }

        public int SesionClaseId { get; set; }

        public SesionClase? SesionClase { get; set; }

        public int EstudianteId { get; set; }

        public Estudiante? Estudiante { get; set; }

        public string EstadoAsistencia { get; set; } = string.Empty;

        public string? Observaciones { get; set; }
    }
}