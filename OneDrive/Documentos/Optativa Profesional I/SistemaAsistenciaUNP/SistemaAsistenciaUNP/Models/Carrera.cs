namespace SistemaAsistenciaUNP.Models
{
    public class Carrera
    {
        public int CarreraId { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int? CentroUniversitarioId { get; set; }

        public CentroUniversitario? CentroUniversitario { get; set; }

        public bool Estado { get; set; } = true;
    }
}