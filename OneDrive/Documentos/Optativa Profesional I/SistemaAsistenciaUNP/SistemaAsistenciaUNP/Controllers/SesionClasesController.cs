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
    public class SesionClasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SesionClasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SesionClases
        public async Task<IActionResult> Index(string buscar, int pagina = 1)
        {
            int tamanoPagina = 5;

            var sesiones = _context.SesionesClase
                .Include(s => s.Grupo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                sesiones = sesiones.Where(s =>
                    s.Tema.Contains(buscar) ||
                    s.Grupo.NombreGrupo.Contains(buscar));
            }

            int totalRegistros = await sesiones.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var sesionesPaginadas = await sesiones
                .OrderBy(s => s.SesionClaseId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            ViewData["Buscar"] = buscar;
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;

            return View(sesionesPaginadas);
        }

        // GET: SesionClases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionClase = await _context.SesionesClase
                .Include(s => s.Grupo)
                .FirstOrDefaultAsync(m => m.SesionClaseId == id);
            if (sesionClase == null)
            {
                return NotFound();
            }

            return View(sesionClase);
        }

        // GET: SesionClases/Create
        public IActionResult Create()
        {
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo");
            return View();
        }

        // POST: SesionClases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SesionClaseId,Fecha,Tema,GrupoId,Estado")] SesionClase sesionClase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesionClase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", sesionClase.GrupoId);
            return View(sesionClase);
        }

        // GET: SesionClases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionClase = await _context.SesionesClase.FindAsync(id);
            if (sesionClase == null)
            {
                return NotFound();
            }
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", sesionClase.GrupoId);
            return View(sesionClase);
        }

        // POST: SesionClases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SesionClaseId,Fecha,Tema,GrupoId,Estado")] SesionClase sesionClase)
        {
            if (id != sesionClase.SesionClaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesionClase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesionClaseExists(sesionClase.SesionClaseId))
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
            ViewData["GrupoId"] = new SelectList(_context.Grupos, "GrupoId", "NombreGrupo", sesionClase.GrupoId);
            return View(sesionClase);
        }

        // GET: SesionClases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesionClase = await _context.SesionesClase
                .Include(s => s.Grupo)
                .FirstOrDefaultAsync(m => m.SesionClaseId == id);
            if (sesionClase == null)
            {
                return NotFound();
            }

            return View(sesionClase);
        }

        // POST: SesionClases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesionClase = await _context.SesionesClase.FindAsync(id);
            if (sesionClase != null)
            {
                _context.SesionesClase.Remove(sesionClase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesionClaseExists(int id)
        {
            return _context.SesionesClase.Any(e => e.SesionClaseId == id);
        }
    }
}
