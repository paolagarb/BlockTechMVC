﻿using System;
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
            var applicationDbContext = _context.Transacao.Include(t => t.CriptomoedaHoje);
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
                .Include(t => t.CriptomoedaHoje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        // GET: Transacoes/Create
        public IActionResult Create()
        {
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.Set<CriptomoedaHoje>(), "Id", "Id");
            return View();
        }

        // POST: Transacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipo,Data,Valor,CriptomoedaHojeId,ApplicationUserId")] Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.Set<CriptomoedaHoje>(), "Id", "Id", transacao.CriptomoedaHojeId);
            return View(transacao);
        }

        // GET: Transacoes/Edit/5
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
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.Set<CriptomoedaHoje>(), "Id", "Id", transacao.CriptomoedaHojeId);
            return View(transacao);
        }

        // POST: Transacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipo,Data,Valor,CriptomoedaHojeId,ApplicationUserId")] Transacao transacao)
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
            ViewData["CriptomoedaHojeId"] = new SelectList(_context.Set<CriptomoedaHoje>(), "Id", "Id", transacao.CriptomoedaHojeId);
            return View(transacao);
        }

        // GET: Transacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacao
                .Include(t => t.CriptomoedaHoje)
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
    }
}