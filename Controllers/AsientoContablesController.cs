using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion24.Data;
using Facturacion24.Models;

namespace Facturacion24.Controllers
{
    public class AsientoContablesController : Controller
    {
        private readonly DbContexto _context;

        public AsientoContablesController(DbContexto context)
        {
            _context = context;
        }

        // GET: AsientoContables
        public async Task<IActionResult> Index()
        {
              return _context.AsientosContables != null ? 
                          View(await _context.AsientosContables.ToListAsync()) :
                          Problem("Entity set 'DbContexto.AsientosContables'  is null.");
        }

        // GET: AsientoContables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AsientosContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // GET: AsientoContables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AsientoContables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,IdCliente,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado")] AsientoContable asientoContable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asientoContable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asientoContable);
        }

        // GET: AsientoContables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AsientosContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables.FindAsync(id);
            if (asientoContable == null)
            {
                return NotFound();
            }
            return View(asientoContable);
        }

        // POST: AsientoContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,IdCliente,Cuenta,TipoMovimiento,FechaAsiento,MontoAsiento,Estado")] AsientoContable asientoContable)
        {
            if (id != asientoContable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asientoContable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoContableExists(asientoContable.Id))
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
            return View(asientoContable);
        }

        // GET: AsientoContables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AsientosContables == null)
            {
                return NotFound();
            }

            var asientoContable = await _context.AsientosContables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            return View(asientoContable);
        }

        // POST: AsientoContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AsientosContables == null)
            {
                return Problem("Entity set 'DbContexto.AsientosContables'  is null.");
            }
            var asientoContable = await _context.AsientosContables.FindAsync(id);
            if (asientoContable != null)
            {
                _context.AsientosContables.Remove(asientoContable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoContableExists(int id)
        {
          return (_context.AsientosContables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
