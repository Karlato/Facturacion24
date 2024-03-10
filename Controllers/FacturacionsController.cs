using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion24.Data;
using Facturacion24.Models;
using System.Diagnostics;

namespace Facturacion24.Controllers
{
    public class FacturacionsController : Controller
    {
        private readonly DbContexto _context;

        public FacturacionsController(DbContexto context)
        {
            _context = context;
        }

        // GET: Facturacions
        public async Task<IActionResult> Index()
        {
              return _context.Facturaciones != null ? 
                          View(await _context.Facturaciones.ToListAsync()) :
                          Problem("Entity set 'DbContexto.Facturaciones'  is null.");
        }

        // GET: Facturacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Facturaciones == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // GET: Facturacions/Create
        public IActionResult Create()
        {
            var vendedores = _context.Vendedores.ToList();
            var clientes = _context.Clientes.ToList();
            var articulos = _context.Articulos.ToList();

            ViewBag.Vendedores = new SelectList(vendedores, "Id", "Nombre");
            ViewBag.Clientes = new SelectList(clientes, "Id", "NombreORazon");
            ViewBag.Articulos = new SelectList(articulos, "Id", "Descripcion");

            return View();
        }

        // POST: Facturacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdVendedor,IdCliente,Fecha,Comentario,IdArticulo,Cantidad,PrecioUnitario")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                var articuloSeleccionado = await _context.Articulos.FindAsync(facturacion.IdArticulo);
                if (articuloSeleccionado != null)
                {
                    facturacion.PrecioUnitario = facturacion.Cantidad * articuloSeleccionado.PrecioUnitario;

                    _context.Add(facturacion);
                    await _context.SaveChangesAsync();

                    Debug.WriteLine("Facturación creada exitosamente.");

                    return RedirectToAction(nameof(Index));
                }
            }

            var vendedores = _context.Vendedores.ToList();
            var clientes = _context.Clientes.ToList();
            var articulos = _context.Articulos.ToList();

            ViewBag.Vendedores = new SelectList(vendedores, "Id", "Nombre", facturacion.IdVendedor);
            ViewBag.Clientes = new SelectList(clientes, "Id", "NombreORazon", facturacion.IdCliente);
            ViewBag.Articulos = new SelectList(articulos, "Id", "Descripcion", facturacion.IdArticulo);

            return View(facturacion);
        }

        // GET: Facturacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Facturaciones == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturaciones.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdVendedor,IdCliente,Fecha,Comentario,IdArticulo,Cantidad,PrecioUnitario")] Facturacion facturacion)
        {
            if (id != facturacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionExists(facturacion.Id))
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
            return View(facturacion);
        }

        // GET: Facturacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Facturaciones == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // POST: Facturacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Facturaciones == null)
            {
                return Problem("Entity set 'DbContexto.Facturaciones'  is null.");
            }
            var facturacion = await _context.Facturaciones.FindAsync(id);
            if (facturacion != null)
            {
                _context.Facturaciones.Remove(facturacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionExists(int id)
        {
          return (_context.Facturaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
