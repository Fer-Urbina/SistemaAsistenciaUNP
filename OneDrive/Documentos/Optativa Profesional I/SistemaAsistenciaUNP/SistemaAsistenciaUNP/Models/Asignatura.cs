namespace SistemaAsistenciaUNP.Models
{
    public class Asignatura
    {
        public int AsignaturaId { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public int Creditos { get; set; }

        public int CarreraId { get; set; }

        public Carrera? Carrera { get; set; }

        public bool Estado { get; set; } = true;
    }
}