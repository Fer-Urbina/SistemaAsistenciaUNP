using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAsistenciaUNP.Data;
using SistemaAsistenciaUNP.Models;

namespace SistemaAsistenciaUNP.Controllers
{
    public class AsistenciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index(string buscar, int pagina = 1)
        {
            int tamanoPagina = 5;

            var asistencias = _context.Asistencias
                .Include(a => a.SesionClase)
                .Include(a => a.Estudiante)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                asistencias = asistencias.Where(a =>
                    a.SesionClase.Tema.Contains(buscar) ||
                    a.Estudiante.Nombres.Contains(buscar) ||
                    a.Estudiante.Apellidos.Contains(buscar) ||
                    a.EstadoAsistencia.Contains(buscar) ||
                    a.Observaciones.Contains(buscar));
            }

            int totalRegistros = await asistencias.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var asistenciasPaginadas = await asistencias
                .OrderBy(a => a.AsistenciaId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            ViewData["Buscar"] = buscar;
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;

            return View(asistenciasPaginadas);
        }

        // GET: Asistencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Estudiante)
                .Include(a => a.SesionClase)
                .FirstOrDefaultAsync(m => m.AsistenciaId == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // GET: Asistencias/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres");
            ViewData["SesionClaseId"] = new SelectList(_context.SesionesClase, "SesionClaseId", "Tema");
            return View();
        }

        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsistenciaId,SesionClaseId,EstudianteId,EstadoAsistencia,Observaciones")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", asistencia.EstudianteId);
            ViewData["SesionClaseId"] = new SelectList(_context.SesionesClase, "SesionClaseId", "Tema", asistencia.SesionClaseId);
            return View(asistencia);
        }

        // GET: Asistencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", asistencia.EstudianteId);
            ViewData["SesionClaseId"] = new SelectList(_context.SesionesClase, "SesionClaseId", "Tema", asistencia.SesionClaseId);
            return View(asistencia);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsistenciaId,SesionClaseId,EstudianteId,EstadoAsistencia,Observaciones")] Asistencia asistencia)
        {
            if (id != asistencia.AsistenciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.AsistenciaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", asistencia.EstudianteId);
            ViewData["SesionClaseId"] = new SelectList(_context.SesionesClase, "SesionClaseId", "Tema", asistencia.SesionClaseId);
            return View(asistencia);
        }

        // GET: Asistencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Estudiante)
                .Include(a => a.SesionClase)
                .FirstOrDefaultAsync(m => m.AsistenciaId == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencias.Remove(asistencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.AsistenciaId == id);
        }
    }
}
