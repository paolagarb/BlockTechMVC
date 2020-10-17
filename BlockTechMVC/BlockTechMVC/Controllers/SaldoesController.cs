using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;

namespace BlockTechMVC.Controllers
{
    public class SaldoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaldoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Saldoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Saldo.ToListAsync());
        }

        // GET: Saldoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldo = await _context.Saldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saldo == null)
            {
                return NotFound();
            }

            return View(saldo);
        }

        // GET: Saldoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Saldoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SaldoAtualRS,QuantidadeCripo")] Saldo saldo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saldo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saldo);
        }

        // GET: Saldoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldo = await _context.Saldo.FindAsync(id);
            if (saldo == null)
            {
                return NotFound();
            }
            return View(saldo);
        }

        // POST: Saldoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SaldoAtualRS,QuantidadeCripo")] Saldo saldo)
        {
            if (id != saldo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saldo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaldoExists(saldo.Id))
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
            return View(saldo);
        }

        // GET: Saldoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldo = await _context.Saldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saldo == null)
            {
                return NotFound();
            }

            return View(saldo);
        }

        // POST: Saldoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saldo = await _context.Saldo.FindAsync(id);
            _context.Saldo.Remove(saldo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaldoExists(int id)
        {
            return _context.Saldo.Any(e => e.Id == id);
        }
    }
}
