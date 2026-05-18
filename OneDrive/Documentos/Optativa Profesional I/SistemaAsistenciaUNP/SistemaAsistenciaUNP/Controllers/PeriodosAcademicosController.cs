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
    public class PeriodosAcademicosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeriodosAcademicosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PeriodosAcademicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeriodosAcademicos.ToListAsync());
        }

        // GET: PeriodosAcademicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoAcademico = await _context.PeriodosAcademicos
                .FirstOrDefaultAsync(m => m.PeriodoAcademicoId == id);
            if (periodoAcademico == null)
            {
                return NotFound();
            }

            return View(periodoAcademico);
        }

        // GET: PeriodosAcademicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeriodosAcademicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeriodoAcademicoId,Nombre,FechaInicio,FechaFin,Estado")] PeriodoAcademico periodoAcademico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodoAcademico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(periodoAcademico);
        }

        // GET: PeriodosAcademicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoAcademico = await _context.PeriodosAcademicos.FindAsync(id);
            if (periodoAcademico == null)
            {
                return NotFound();
            }
            return View(periodoAcademico);
        }

        // POST: PeriodosAcademicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeriodoAcademicoId,Nombre,FechaInicio,FechaFin,Estado")] PeriodoAcademico periodoAcademico)
        {
            if (id != periodoAcademico.PeriodoAcademicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodoAcademico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodoAcademicoExists(periodoAcademico.PeriodoAcademicoId))
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
            return View(periodoAcademico);
        }

        // GET: PeriodosAcademicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodoAcademico = await _context.PeriodosAcademicos
                .FirstOrDefaultAsync(m => m.PeriodoAcademicoId == id);
            if (periodoAcademico == null)
            {
                return NotFound();
            }

            return View(periodoAcademico);
        }

        // POST: PeriodosAcademicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodoAcademico = await _context.PeriodosAcademicos.FindAsync(id);
            if (periodoAcademico != null)
            {
                _context.PeriodosAcademicos.Remove(periodoAcademico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodoAcademicoExists(int id)
        {
            return _context.PeriodosAcademicos.Any(e => e.PeriodoAcademicoId == id);
        }
    }
}
