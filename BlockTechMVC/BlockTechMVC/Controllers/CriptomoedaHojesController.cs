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
    public class CriptomoedaHojesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedaHojesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CriptomoedaHojes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CriptomoedaHoje.Include(c => c.Criptomoeda);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CriptomoedaHojes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje
                .Include(c => c.Criptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoedaHoje == null)
            {
                return NotFound();
            }

            return View(criptomoedaHoje);
        }

        // GET: CriptomoedaHojes/Create
        public IActionResult Create()
        {
            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Id");
            return View();
        }

        // POST: CriptomoedaHojes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criptomoedaHoje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Id", criptomoedaHoje.CriptomoedaId);
            return View(criptomoedaHoje);
        }

        // GET: CriptomoedaHojes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje.FindAsync(id);
            if (criptomoedaHoje == null)
            {
                return NotFound();
            }
            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Id", criptomoedaHoje.CriptomoedaId);
            return View(criptomoedaHoje);
        }

        // POST: CriptomoedaHojes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            if (id != criptomoedaHoje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criptomoedaHoje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriptomoedaHojeExists(criptomoedaHoje.Id))
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
            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Id", criptomoedaHoje.CriptomoedaId);
            return View(criptomoedaHoje);
        }

        // GET: CriptomoedaHojes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje
                .Include(c => c.Criptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoedaHoje == null)
            {
                return NotFound();
            }

            return View(criptomoedaHoje);
        }

        // POST: CriptomoedaHojes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criptomoedaHoje = await _context.CriptomoedaHoje.FindAsync(id);
            _context.CriptomoedaHoje.Remove(criptomoedaHoje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriptomoedaHojeExists(int id)
        {
            return _context.CriptomoedaHoje.Any(e => e.Id == id);
        }
    }
}
