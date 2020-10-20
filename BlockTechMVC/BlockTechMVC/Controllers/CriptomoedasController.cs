using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class CriptomoedasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Criptomoedas
        public async Task<IActionResult> Index(string searchString)
        {
            var criptomoedas = from c in _context.Criptomoeda
                               select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                criptomoedas = criptomoedas.Where(c => c.Nome.Contains(searchString));
            }

            return View(await criptomoedas.ToListAsync());
        }

        // GET: Criptomoedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoeda = await _context.Criptomoeda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoeda == null)
            {
                return NotFound();
            }

            return View(criptomoeda);
        }

        // GET: Criptomoedas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Criptomoedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criptomoeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criptomoeda);
        }

        // GET: Criptomoedas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoeda = await _context.Criptomoeda.FindAsync(id);
            if (criptomoeda == null)
            {
                return NotFound();
            }
            return View(criptomoeda);
        }

        // POST: Criptomoedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (id != criptomoeda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criptomoeda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriptomoedaExists(criptomoeda.Id))
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
            return View(criptomoeda);
        }

        // GET: Criptomoedas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criptomoeda = await _context.Criptomoeda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoeda == null)
            {
                return NotFound();
            }

            return View(criptomoeda);
        }

        // POST: Criptomoedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criptomoeda = await _context.Criptomoeda.FindAsync(id);
            _context.Criptomoeda.Remove(criptomoeda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriptomoedaExists(int id)
        {
            return _context.Criptomoeda.Any(e => e.Id == id);
        }
    }
}
