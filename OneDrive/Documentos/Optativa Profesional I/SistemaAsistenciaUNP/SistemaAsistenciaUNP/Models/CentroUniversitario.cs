namespace SistemaAsistenciaUNP.Models
{
    public class CentroUniversitario
    {
        public int CentroUniversitarioId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public bool Estado { get; set; } = true;
    }
}