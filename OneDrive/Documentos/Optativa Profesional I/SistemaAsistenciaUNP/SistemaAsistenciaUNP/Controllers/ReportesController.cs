using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAsistenciaUNP.Data;
using SistemaAsistenciaUNP.ViewModels;
using System.Text;
using Rotativa.AspNetCore;

namespace SistemaAsistenciaUNP.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin, int? carreraId, string? aula, string? estado)
        {
            await CargarFiltros();

            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");
            ViewBag.CarreraId = carreraId;
            ViewBag.Aula = aula;
            ViewBag.Estado = estado;

            ViewBag.TotalCarreras = await _context.Carreras.CountAsync();
            ViewBag.TotalDocentes = await _context.Docentes.CountAsync();
            ViewBag.TotalEstudiantes = await _context.Estudiantes.CountAsync();
            ViewBag.TotalAsignaturas = await _context.Asignaturas.CountAsync();
            ViewBag.TotalGrupos = await _context.Grupos.CountAsync();
            ViewBag.TotalMatriculas = await _context.Matriculas.CountAsync();
            ViewBag.TotalSesiones = await _context.SesionesClase.CountAsync();
            ViewBag.TotalAsistencias = await _context.Asistencias.CountAsync();

            var query = _context.Asistencias
                .Include(a => a.Estudiante)
                    .ThenInclude(e => e.Carrera)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Asignatura)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Docente)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Aula)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date >= fechaInicio.Value.Date);

            if (fechaFin.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date <= fechaFin.Value.Date);

            if (carreraId.HasValue)
                query = query.Where(a => a.Estudiante!.CarreraId == carreraId.Value);

            if (!string.IsNullOrWhiteSpace(aula))
                query = query.Where(a => a.SesionClase!.Grupo!.Aula!.Nombre == aula);

            if (!string.IsNullOrWhiteSpace(estado))
                query = query.Where(a => a.EstadoAsistencia == estado);

            var resultados = await query
                .OrderByDescending(a => a.SesionClase!.Fecha)
                .Select(a => new ReporteAsistenciaViewModel
                {
                    Fecha = a.SesionClase!.Fecha,
                    Estudiante = a.Estudiante!.Nombres + " " + a.Estudiante.Apellidos,
                    Carrera = a.Estudiante.Carrera!.Nombre,
                    Asignatura = a.SesionClase.Grupo!.Asignatura!.Nombre,
                    Grupo = a.SesionClase.Grupo.NombreGrupo,
                    Docente = a.SesionClase.Grupo.Docente!.Nombres + " " + a.SesionClase.Grupo.Docente.Apellidos,
                    Aula = a.SesionClase.Grupo.Aula != null ? a.SesionClase.Grupo.Aula.Nombre : "No asignada",
                    EstadoAsistencia = a.EstadoAsistencia,
                    Observaciones = a.Observaciones
                })
                .ToListAsync();

            ViewBag.TotalPresentes = resultados.Count(r => r.EstadoAsistencia == "Presente");
            ViewBag.TotalAusentes = resultados.Count(r => r.EstadoAsistencia == "Ausente");
            ViewBag.TotalJustificados = resultados.Count(r => r.EstadoAsistencia == "Justificado");

            return View(resultados);
        }

        public async Task<IActionResult> ExportarCsv(DateTime? fechaInicio, DateTime? fechaFin, int? carreraId, string? aula, string? estado)
        {
            var query = _context.Asistencias
                .Include(a => a.Estudiante)
                    .ThenInclude(e => e.Carrera)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Asignatura)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Docente)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Aula)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date >= fechaInicio.Value.Date);

            if (fechaFin.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date <= fechaFin.Value.Date);

            if (carreraId.HasValue)
                query = query.Where(a => a.Estudiante!.CarreraId == carreraId.Value);

            if (!string.IsNullOrWhiteSpace(aula))
                query = query.Where(a => a.SesionClase!.Grupo!.Aula!.Nombre == aula);

            if (!string.IsNullOrWhiteSpace(estado))
                query = query.Where(a => a.EstadoAsistencia == estado);

            var datos = await query
                .OrderByDescending(a => a.SesionClase!.Fecha)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Fecha,Estudiante,Carrera,Asignatura,Grupo,Docente,Aula,Estado,Observaciones");

            foreach (var a in datos)
            {
                csv.AppendLine($"{a.SesionClase!.Fecha:dd/MM/yyyy}," +
                               $"{a.Estudiante!.Nombres} {a.Estudiante.Apellidos}," +
                               $"{a.Estudiante.Carrera!.Nombre}," +
                               $"{a.SesionClase.Grupo!.Asignatura!.Nombre}," +
                               $"{a.SesionClase.Grupo.NombreGrupo}," +
                               $"{a.SesionClase.Grupo.Docente!.Nombres} {a.SesionClase.Grupo.Docente.Apellidos}," +
                               $"{a.SesionClase.Grupo.Aula?.Nombre ?? "No asignada"}," +
                               $"{a.EstadoAsistencia}," +
                               $"{a.Observaciones}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "ReporteAsistencias.csv");
        }

        public async Task<IActionResult> ExportarPdf(DateTime? fechaInicio, DateTime? fechaFin, int? carreraId, string? aula, string? estado)
        {
            var query = _context.Asistencias
                .Include(a => a.Estudiante)
                    .ThenInclude(e => e.Carrera)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Asignatura)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Docente)
                .Include(a => a.SesionClase)
                    .ThenInclude(s => s.Grupo)
                        .ThenInclude(g => g.Aula)
                .AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date >= fechaInicio.Value.Date);

            if (fechaFin.HasValue)
                query = query.Where(a => a.SesionClase!.Fecha.Date <= fechaFin.Value.Date);

            if (carreraId.HasValue)
                query = query.Where(a => a.Estudiante!.CarreraId == carreraId.Value);

            if (!string.IsNullOrWhiteSpace(aula))
                query = query.Where(a => a.SesionClase!.Grupo!.Aula!.Nombre == aula);

            if (!string.IsNullOrWhiteSpace(estado))
                query = query.Where(a => a.EstadoAsistencia == estado);

            var asistencias = await query
                .OrderByDescending(a => a.SesionClase!.Fecha)
                .ToListAsync();

            return new ViewAsPdf("ReportePdf", asistencias)
            {
                FileName = "ReporteAsistencias.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        private async Task CargarFiltros()
        {
            ViewBag.Carreras = new SelectList(await _context.Carreras.OrderBy(c => c.Nombre).ToListAsync(), "CarreraId", "Nombre");

            ViewBag.Aulas = await _context.Grupos
                .Where(g => g.Aula != null && g.Aula.Nombre != "")
                .Select(g => g.Aula!.Nombre)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();

            ViewBag.Estados = new List<string> { "Presente", "Ausente", "Justificado" };
        }
    }
}