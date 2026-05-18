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
    public class CentroUniversitariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CentroUniversitariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CentroUniversitarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.CentrosUniversitarios.ToListAsync());
        }

        // GET: CentroUniversitarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroUniversitario = await _context.CentrosUniversitarios
                .FirstOrDefaultAsync(m => m.CentroUniversitarioId == id);
            if (centroUniversitario == null)
            {
                return NotFound();
            }

            return View(centroUniversitario);
        }

        // GET: CentroUniversitarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CentroUniversitarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CentroUniversitarioId,Nombre,Direccion,Telefono,Estado")] CentroUniversitario centroUniversitario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centroUniversitario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centroUniversitario);
        }

        // GET: CentroUniversitarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroUniversitario = await _context.CentrosUniversitarios.FindAsync(id);
            if (centroUniversitario == null)
            {
                return NotFound();
            }
            return View(centroUniversitario);
        }

        // POST: CentroUniversitarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CentroUniversitarioId,Nombre,Direccion,Telefono,Estado")] CentroUniversitario centroUniversitario)
        {
            if (id != centroUniversitario.CentroUniversitarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centroUniversitario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentroUniversitarioExists(centroUniversitario.CentroUniversitarioId))
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
            return View(centroUniversitario);
        }

        // GET: CentroUniversitarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroUniversitario = await _context.CentrosUniversitarios
                .FirstOrDefaultAsync(m => m.CentroUniversitarioId == id);
            if (centroUniversitario == null)
            {
                return NotFound();
            }

            return View(centroUniversitario);
        }

        // POST: CentroUniversitarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centroUniversitario = await _context.CentrosUniversitarios.FindAsync(id);
            if (centroUniversitario != null)
            {
                _context.CentrosUniversitarios.Remove(centroUniversitario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentroUniversitarioExists(int id)
        {
            return _context.CentrosUniversitarios.Any(e => e.CentroUniversitarioId == id);
        }
    }
}
