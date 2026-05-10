namespace SistemaAsistenciaUNP.Models
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }

        public string Carnet { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int CarreraId { get; set; }

        public Carrera? Carrera { get; set; }

        public bool Estado { get; set; } = true;
    }
}