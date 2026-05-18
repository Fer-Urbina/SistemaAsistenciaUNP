namespace SistemaAsistenciaUNP.Models
{
    public class SesionClase
    {
        public int SesionClaseId { get; set; }

        public DateTime Fecha { get; set; }

        public string? Tema { get; set; }

        public int GrupoId { get; set; }

        public Grupo? Grupo { get; set; }

        public bool Estado { get; set; } = true;
    }
}