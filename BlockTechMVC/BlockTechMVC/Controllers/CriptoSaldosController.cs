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
    public class CriptoSaldosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriptoSaldosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CriptoSaldos
        public async Task<IActionResult> Index()
        {
            return View(await _context.CriptoSaldo.ToListAsync());
        }

        // GET: CriptoSaldos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptoSaldo = await _context.CriptoSaldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptoSaldo == null)
            {
                return NotFound();
            }

            return View(criptoSaldo);
        }

        // GET: CriptoSaldos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CriptoSaldos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Criptomoeda,Quantidade")] CriptoSaldo criptoSaldo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criptoSaldo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criptoSaldo);
        }

        // GET: CriptoSaldos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptoSaldo = await _context.CriptoSaldo.FindAsync(id);
            if (criptoSaldo == null)
            {
                return NotFound();
            }
            return View(criptoSaldo);
        }

        // POST: CriptoSaldos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Criptomoeda,Quantidade")] CriptoSaldo criptoSaldo)
        {
            if (id != criptoSaldo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criptoSaldo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriptoSaldoExists(criptoSaldo.Id))
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
            return View(criptoSaldo);
        }

        // GET: CriptoSaldos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptoSaldo = await _context.CriptoSaldo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptoSaldo == null)
            {
                return NotFound();
            }

            return View(criptoSaldo);
        }

        // POST: CriptoSaldos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criptoSaldo = await _context.CriptoSaldo.FindAsync(id);
            _context.CriptoSaldo.Remove(criptoSaldo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriptoSaldoExists(int id)
        {
            return _context.CriptoSaldo.Any(e => e.Id == id);
        }
    }
}
