namespace SistemaAsistenciaUNP.Models
{
    public class Docente
    {
        public int DocenteId { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Especialidad { get; set; }

        public bool Estado { get; set; } = true;
    }
}