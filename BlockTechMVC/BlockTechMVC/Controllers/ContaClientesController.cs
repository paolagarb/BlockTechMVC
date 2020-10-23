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
    public class ContaClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContaClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContaClientes
        public async Task<IActionResult> Index()
        {
            var contas = _context.ContaCliente
                .Include(c => c.Conta)
                .Include(c => c.ApplicationUser);


            return View(contas.ToList());
        }

        // GET: ContaClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaCliente
                .Include(c => c.Conta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCliente == null)
            {
                return NotFound();
            }

            return View(contaCliente);
        }

        // GET: ContaClientes/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ContaId"] = new SelectList(_context.Conta, "Id", "Id");
            return View();
        }

        // POST: ContaClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,NumeroConta,DataAbertura,ApplicationUserID,ContaId")] ContaCliente contaCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contaCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContaId"] = new SelectList(_context.Conta, "Id", "Id", contaCliente.ContaId);
            return View(contaCliente);
        }

        // GET: ContaClientes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaCliente.FindAsync(id);
            if (contaCliente == null)
            {
                return NotFound();
            }
            ViewData["ContaId"] = new SelectList(_context.Conta, "Id", "Id", contaCliente.ContaId);
            return View(contaCliente);
        }

        // POST: ContaClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroConta,DataAbertura,ApplicationUserID,ContaId")] ContaCliente contaCliente)
        {
            if (id != contaCliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contaCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaClienteExists(contaCliente.Id))
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
            ViewData["ContaId"] = new SelectList(_context.Conta, "Id", "Id", contaCliente.ContaId);
            return View(contaCliente);
        }

        // GET: ContaClientes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contaCliente = await _context.ContaCliente
                .Include(c => c.Conta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contaCliente == null)
            {
                return NotFound();
            }

            return View(contaCliente);
        }

        // POST: ContaClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contaCliente = await _context.ContaCliente.FindAsync(id);
            _context.ContaCliente.Remove(contaCliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContaClienteExists(int id)
        {
            return _context.ContaCliente.Any(e => e.Id == id);
        }
    }
}
