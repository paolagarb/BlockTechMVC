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
using Microsoft.AspNetCore.Identity;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class TransacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transacoes
        public async Task<IActionResult> Index()
        {
            //if (User.Identity.Name != "Administrador") {
            //    var user = User.Identity.Name;
            //    applicationDbContext = _context.Transacao
            //        .Where(c => c.ContaCliente.ApplicationUser.Nome.Equals(user))
            //        .Include(t => t.CriptomoedaHoje);
            //}

            //var user = _context.ApplicationUser.FirstOrDefault(c => c.Nome == User.Identity.Name);
            //if (user != null)
            //{
            //    return View(await user.)
            //}

            var applicationDbContext = _context.Transacao
                .Include(t => t.ContaCliente)
                .Include(t => t.CriptoSaldo)
                .Include(t => t.CriptomoedaHoje)
                .Include(t => t.Saldo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacao
                .Include(t => t.ContaCliente)
                .Include(t => t.CriptoSaldo)
                .Include(t => t.CriptomoedaHoje)
                .Include(t => t.Saldo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        // GET: Transacoes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            ViewData["ContaClienteId"] = new SelectList(_context.ContaCliente, "Id", "Id");
            ViewData["CriptoSaldoId"] = new SelectList(_context.CriptoSaldo, "Id", "Id");
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.CriptomoedaHoje, "Id", "Id");
            ViewData["SaldoId"] = new SelectList(_context.Saldo, "Id", "Id");
            return View();
        }

        // POST: Transacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Data,Valor,CriptomoedaHojeId,ContaClienteId,CriptoSaldoId,SaldoId")] Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            //Transacao t1 = new Transacao(/*)*/
            

            ViewData["ContaClienteId"] = new SelectList(_context.ContaCliente, "Id", "Id", transacao.ContaClienteId);
            ViewData["CriptoSaldoId"] = new SelectList(_context.CriptoSaldo, "Id", "Id", transacao.CriptoSaldoId);
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.CriptomoedaHoje, "Id", "Id", transacao.CriptomoedaHojeId);
            ViewData["SaldoId"] = new SelectList(_context.Saldo, "Id", "Id", transacao.SaldoId);
            return View(transacao);
        }

        // GET: Transacoes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacao.FindAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }
            ViewData["ContaClienteId"] = new SelectList(_context.ContaCliente, "Id", "Id", transacao.ContaClienteId);
            ViewData["CriptoSaldoId"] = new SelectList(_context.CriptoSaldo, "Id", "Id", transacao.CriptoSaldoId);
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.CriptomoedaHoje, "Id", "Id", transacao.CriptomoedaHojeId);
            ViewData["SaldoId"] = new SelectList(_context.Saldo, "Id", "Id", transacao.SaldoId);
            return View(transacao);
        }

        // POST: Transacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Data,Valor,CriptomoedaHojeId,ContaClienteId,CriptoSaldoId,SaldoId")] Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransacaoExists(transacao.Id))
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
            ViewData["ContaClienteId"] = new SelectList(_context.ContaCliente, "Id", "Id", transacao.ContaClienteId);
            ViewData["CriptoSaldoId"] = new SelectList(_context.CriptoSaldo, "Id", "Id", transacao.CriptoSaldoId);
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.CriptomoedaHoje, "Id", "Id", transacao.CriptomoedaHojeId);
            ViewData["SaldoId"] = new SelectList(_context.Saldo, "Id", "Id", transacao.SaldoId);
            return View(transacao);
        }

        // GET: Transacoes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacao
                .Include(t => t.ContaCliente)
                .Include(t => t.CriptoSaldo)
                .Include(t => t.CriptomoedaHoje)
                .Include(t => t.Saldo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        // POST: Transacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transacao = await _context.Transacao.FindAsync(id);
            _context.Transacao.Remove(transacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Relatorios()
        {
            var temporario = _context.Transacao;

            return View(temporario.ToListAsync());
        }
    }
}
