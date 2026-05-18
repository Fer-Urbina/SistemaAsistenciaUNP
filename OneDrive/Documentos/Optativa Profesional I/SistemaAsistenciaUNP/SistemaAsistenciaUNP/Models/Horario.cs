namespace SistemaAsistenciaUNP.Models
{
    public class Horario
    {
        public int HorarioId { get; set; }

        public string DiaSemana { get; set; } = string.Empty;

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public bool Estado { get; set; } = true;
    }
}