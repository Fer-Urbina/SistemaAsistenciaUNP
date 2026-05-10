using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAsistenciaUNP.Data;

namespace SistemaAsistenciaUNP.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalCarreras = await _context.Carreras.CountAsync();
            ViewBag.TotalDocentes = await _context.Docentes.CountAsync();
            ViewBag.TotalEstudiantes = await _context.Estudiantes.CountAsync();
            ViewBag.TotalAsignaturas = await _context.Asignaturas.CountAsync();
            ViewBag.TotalGrupos = await _context.Grupos.CountAsync();
            ViewBag.TotalMatriculas = await _context.Matriculas.CountAsync();
            ViewBag.TotalSesiones = await _context.SesionesClase.CountAsync();
            ViewBag.TotalAsistencias = await _context.Asistencias.CountAsync();

            ViewBag.TotalPresentes = await _context.Asistencias
                .CountAsync(a => a.EstadoAsistencia == "Presente");

            ViewBag.TotalAusentes = await _context.Asistencias
                .CountAsync(a => a.EstadoAsistencia == "Ausente");

            ViewBag.TotalJustificados = await _context.Asistencias
                .CountAsync(a => a.EstadoAsistencia == "Justificado");

            return View();
        }
    }
}