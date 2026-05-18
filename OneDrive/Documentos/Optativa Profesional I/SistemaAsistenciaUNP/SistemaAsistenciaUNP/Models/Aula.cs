namespace SistemaAsistenciaUNP.Models
{
    public class Aula
    {
        public int AulaId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        public string? Ubicacion { get; set; }

        public int CentroUniversitarioId { get; set; }

        public CentroUniversitario? CentroUniversitario { get; set; }

        public bool Estado { get; set; } = true;
    }
}