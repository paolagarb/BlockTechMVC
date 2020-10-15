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
    public class SaldoCriptomoedaHojesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaldoCriptomoedaHojesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SaldoCriptomoedaHojes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SaldoCriptomoedaHoje.Include(s => s.CompraCriptomoeda);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SaldoCriptomoedaHojes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldoCriptomoedaHoje = await _context.SaldoCriptomoedaHoje
                .Include(s => s.CompraCriptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saldoCriptomoedaHoje == null)
            {
                return NotFound();
            }

            return View(saldoCriptomoedaHoje);
        }

        // GET: SaldoCriptomoedaHojes/Create
        public IActionResult Create()
        {
            ViewData["CompraCriptomoedaId"] = new SelectList(_context.Set<CompraCriptomoeda>(), "Id", "Id");
            return View();
        }

        // POST: SaldoCriptomoedaHojes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,CompraCriptomoedaId")] SaldoCriptomoedaHoje saldoCriptomoedaHoje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saldoCriptomoedaHoje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompraCriptomoedaId"] = new SelectList(_context.Set<CompraCriptomoeda>(), "Id", "Id", saldoCriptomoedaHoje.CompraCriptomoedaId);
            return View(saldoCriptomoedaHoje);
        }

        // GET: SaldoCriptomoedaHojes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldoCriptomoedaHoje = await _context.SaldoCriptomoedaHoje.FindAsync(id);
            if (saldoCriptomoedaHoje == null)
            {
                return NotFound();
            }
            ViewData["CompraCriptomoedaId"] = new SelectList(_context.Set<CompraCriptomoeda>(), "Id", "Id", saldoCriptomoedaHoje.CompraCriptomoedaId);
            return View(saldoCriptomoedaHoje);
        }

        // POST: SaldoCriptomoedaHojes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,CompraCriptomoedaId")] SaldoCriptomoedaHoje saldoCriptomoedaHoje)
        {
            if (id != saldoCriptomoedaHoje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saldoCriptomoedaHoje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaldoCriptomoedaHojeExists(saldoCriptomoedaHoje.Id))
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
            ViewData["CompraCriptomoedaId"] = new SelectList(_context.Set<CompraCriptomoeda>(), "Id", "Id", saldoCriptomoedaHoje.CompraCriptomoedaId);
            return View(saldoCriptomoedaHoje);
        }

        // GET: SaldoCriptomoedaHojes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saldoCriptomoedaHoje = await _context.SaldoCriptomoedaHoje
                .Include(s => s.CompraCriptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saldoCriptomoedaHoje == null)
            {
                return NotFound();
            }

            return View(saldoCriptomoedaHoje);
        }

        // POST: SaldoCriptomoedaHojes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saldoCriptomoedaHoje = await _context.SaldoCriptomoedaHoje.FindAsync(id);
            _context.SaldoCriptomoedaHoje.Remove(saldoCriptomoedaHoje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaldoCriptomoedaHojeExists(int id)
        {
            return _context.SaldoCriptomoedaHoje.Any(e => e.Id == id);
        }
    }
}
