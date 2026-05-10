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
    public class MatriculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatriculasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matriculas
        public async Task<IActionResult> Index(string buscar, int pagina = 1)
        {
            int tamanoPagina = 5;

            var matriculas = _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Grupo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                matriculas = matriculas.Where(m =>
                    m.Estudiante.Nombres.Contains(buscar) ||
                    m.Grupo.NombreGrupo.Contains(buscar));
            }

            int totalRegistros = await matriculas.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var matriculasPaginadas = await matriculas
                .OrderBy(m => m.MatriculaId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            ViewData["Buscar"] = buscar;
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;

            return View(matriculasPaginadas);
        }

        // GET: Matriculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Grupo)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matriculas/Create
        public IActionResult Create()
        {
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres");
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatriculaId,EstudianteId,GrupoId,FechaMatricula,Estado")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matricula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", matricula.EstudianteId);

            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", matricula.GrupoId);
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula == null)
            {
                return NotFound();
            }
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", matricula.EstudianteId);
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", matricula.GrupoId);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatriculaId,EstudianteId,GrupoId,FechaMatricula,Estado")] Matricula matricula)
        {
            if (id != matricula.MatriculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.MatriculaId))
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
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "EstudianteId", "Nombres", matricula.EstudianteId);

            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", matricula.GrupoId);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Estudiante)
                .Include(m => m.Grupo)
                .FirstOrDefaultAsync(m => m.MatriculaId == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.MatriculaId == id);
        }
    }
}
