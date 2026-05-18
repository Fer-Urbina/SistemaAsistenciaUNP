using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAsistenciaUNP.Data;
using SistemaAsistenciaUNP.Models;

namespace SistemaAsistenciaUNP.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index(string buscar, int pagina = 1)
        {
            int tamanoPagina = 5;

            var carreras = _context.Carreras
                .Include(c => c.CentroUniversitario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                carreras = carreras.Where(c =>
                    c.Codigo.Contains(buscar) ||
                    c.Nombre.Contains(buscar) ||
                    c.Descripcion.Contains(buscar) ||
                    c.CentroUniversitario.Nombre.Contains(buscar));
            }

            int totalRegistros = await carreras.CountAsync();
            int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

            var carrerasPaginadas = await carreras
                .OrderBy(c => c.CarreraId)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            ViewData["Buscar"] = buscar;
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;

            return View(carrerasPaginadas);
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .Include(c => c.CentroUniversitario)
                .FirstOrDefaultAsync(m => m.CarreraId == id);

            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            ViewData["CentroUniversitarioId"] = new SelectList(
                _context.CentroUniversitarios.Where(c => c.Estado),
                "CentroUniversitarioId",
                "Nombre"
            );

            return View();
        }

        // POST: Carreras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarreraId,Codigo,Nombre,Descripcion,Estado,CentroUniversitarioId")] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrera);
                await _context.SaveChangesAsync();
                return LocalRedirect("/Carreras");
            }

            ViewData["CentroUniversitarioId"] = new SelectList(
                _context.CentroUniversitarios.Where(c => c.Estado),
                "CentroUniversitarioId",
                "Nombre",
                carrera.CentroUniversitarioId
            );

            return View(carrera);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera == null)
            {
                return NotFound();
            }

            ViewData["CentroUniversitarioId"] = new SelectList(
                _context.CentroUniversitarios.Where(c => c.Estado),
                "CentroUniversitarioId",
                "Nombre",
                carrera.CentroUniversitarioId
            );

            return View(carrera);
        }

        // POST: Carreras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarreraId,Codigo,Nombre,Descripcion,Estado,CentroUniversitarioId")] Carrera carrera)
        {
            if (id != carrera.CarreraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.CarreraId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return LocalRedirect("/Carreras");
            }

            ViewData["CentroUniversitarioId"] = new SelectList(
                _context.CentroUniversitarios.Where(c => c.Estado),
                "CentroUniversitarioId",
                "Nombre",
                carrera.CentroUniversitarioId
            );

            return View(carrera);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .Include(c => c.CentroUniversitario)
                .FirstOrDefaultAsync(m => m.CarreraId == id);

            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);

            if (carrera != null)
            {
                _context.Carreras.Remove(carrera);
            }

            await _context.SaveChangesAsync();
            return LocalRedirect("/Carreras");
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.CarreraId == id);
        }
    }
}