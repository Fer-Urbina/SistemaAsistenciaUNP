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
    public class GrupoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrupoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grupoes
        public async Task<IActionResult> Index(string buscar, int pagina = 1)
        {
            int tamanoPagina = 5;

            var grupos = _context.Grupos
                .Include(g => g.Asignatura)
                .Include(g => g.Docente)
                .Include(g => g.Aula)
                .Include(g => g.PeriodoAcademico)
                .Include(g => g.Horario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                grupos = grupos.Where(g =>
                    g.NombreGrupo.Contains(buscar) ||
                    g.Turno.Contains(buscar) ||
                    g.Aula!.Nombre.Contains(buscar) ||
                    g.PeriodoAcademico!.Nombre.Contains(buscar) ||
                    g.Asignatura.Nombre.Contains(buscar) ||
                    g.Docente.Nombres.Contains(buscar));
            }

            int totalRegistros = await grupos.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var gruposPaginados = await grupos
                .OrderBy(g => g.GrupoId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            ViewData["Buscar"] = buscar;
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;

            return View(gruposPaginados);
        }

        // GET: Grupoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.Asignatura)
                .Include(g => g.Docente)
                .Include(g => g.Aula)
                .Include(g => g.PeriodoAcademico)
                .Include(g => g.Horario)
                .FirstOrDefaultAsync(m => m.GrupoId == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // GET: Grupoes/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "Nombre");
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "DocenteId", "Nombres");
            ViewData["AulaId"] = new SelectList(_context.Aulas, "AulaId", "Nombre");
            ViewData["PeriodoAcademicoId"] = new SelectList(_context.PeriodosAcademicos, "PeriodoAcademicoId", "Nombre");
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "HorarioId", "DiaSemana");
            return View();
        }

        // POST: Grupoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrupoId,NombreGrupo,Turno,AulaId,PeriodoAcademicoId,HorarioId,AsignaturaId,DocenteId,Estado")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "Nombre", grupo.AsignaturaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "DocenteId", "Nombres", grupo.DocenteId);
            ViewData["AulaId"] = new SelectList(_context.Aulas, "AulaId", "Nombre", grupo.AulaId);
            ViewData["PeriodoAcademicoId"] = new SelectList(_context.PeriodosAcademicos, "PeriodoAcademicoId", "Nombre", grupo.PeriodoAcademicoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "HorarioId", "DiaSemana", grupo.HorarioId);
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "Nombre", grupo.AsignaturaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "DocenteId", "Nombres", grupo.DocenteId);
            ViewData["AulaId"] = new SelectList(_context.Aulas, "AulaId", "Nombre", grupo.AulaId);
            ViewData["PeriodoAcademicoId"] = new SelectList(_context.PeriodosAcademicos, "PeriodoAcademicoId", "Nombre", grupo.PeriodoAcademicoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "HorarioId", "DiaSemana", grupo.HorarioId);
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrupoId,NombreGrupo,Turno,AulaId,PeriodoAcademicoId,HorarioId,AsignaturaId,DocenteId,Estado")] Grupo grupo)
        {
            if (id != grupo.GrupoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoExists(grupo.GrupoId))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "AsignaturaId", "Nombre", grupo.AsignaturaId);
            ViewData["DocenteId"] = new SelectList(_context.Docentes, "DocenteId", "Nombres", grupo.DocenteId);
            ViewData["AulaId"] = new SelectList(_context.Aulas, "AulaId", "Nombre", grupo.AulaId);
            ViewData["PeriodoAcademicoId"] = new SelectList(_context.PeriodosAcademicos, "PeriodoAcademicoId", "Nombre", grupo.PeriodoAcademicoId);
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "HorarioId", "DiaSemana", grupo.HorarioId);
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.Grupos
                .Include(g => g.Asignatura)
                .Include(g => g.Docente)
                .Include(g => g.Aula)
                .Include(g => g.PeriodoAcademico)
                .Include(g => g.Horario)
                .FirstOrDefaultAsync(m => m.GrupoId == id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo = await _context.Grupos.FindAsync(id);
            if (grupo != null)
            {
                _context.Grupos.Remove(grupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoExists(int id)
        {
            return _context.Grupos.Any(e => e.GrupoId == id);
        }
    }
}
