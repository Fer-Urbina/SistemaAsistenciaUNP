namespace SistemaAsistenciaUNP.Models
{
    public class PeriodoAcademico
    {
        public int PeriodoAcademicoId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Estado { get; set; } = true;
    }
}