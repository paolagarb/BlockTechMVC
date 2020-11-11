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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class CriptomoedasHojeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedasHojeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CriptomoedasHoje
        public async Task<IActionResult> Index(DateTime searchDate, string sortOrder)
        {
            var criptomoedas = _context.CriptomoedaHoje
                .Where(c => c.Data.Equals(DateTime.Now.Date))
                .Include(c => c.Criptomoeda);

            if (searchDate != DateTime.MinValue)
            {
                criptomoedas = _context.CriptomoedaHoje
                .Where(c => c.Data.Equals(searchDate))
                .Include(c => c.Criptomoeda);
            }

            ViewBag.NameSortParm = sortOrder == "Nome" ? "Nome_desc" : "Nome";
            ViewBag.ValueSortParm = sortOrder == "Valor" ? "Valor_desc" : "Valor";

            if (sortOrder != null)
            {

                var criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);
                switch (sortOrder)
                {
                    case "Nome_desc":
                        criptomoeda = criptomoedas.OrderByDescending(c => c.Criptomoeda.Nome);
                        break;
                    case "Valor_desc":
                        criptomoeda = criptomoedas.OrderByDescending(c => c.Valor);
                        break;
                    case "Valor":
                        criptomoeda = criptomoedas.OrderBy(c => c.Valor);
                        break;
                    case "Nome":
                        criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);
                        break;
                    default:
                        criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);
                        break;
                }
                return View(await criptomoeda.ToListAsync());
            }

            return View(await criptomoedas.ToListAsync());
        }

        // GET: CriptomoedasHoje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje
                .Include(c => c.Criptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoedaHoje == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            return View(criptomoedaHoje);
        }

        // GET: CriptomoedasHoje/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Nome");
            ViewData["CriptomoedaIdSimb"] = new SelectList(_context.Criptomoeda, "Id", "Simbolo");
            return View();
        }

        // POST: CriptomoedasHoje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(criptomoedaHoje);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "" });
            }
        }

        // GET: CriptomoedasHoje/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje.FindAsync(id);
            if (criptomoedaHoje == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            ViewData["CriptomoedaId"] = new SelectList(_context.Criptomoeda, "Id", "Nome", criptomoedaHoje.CriptomoedaId);
            ViewData["CriptomoedaIdSimb"] = new SelectList(_context.Criptomoeda, "Id", "Simbolo");
            return View(criptomoedaHoje);
        }

        // POST: CriptomoedasHoje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            if (id != criptomoedaHoje.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
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
                        return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(criptomoedaHoje);
        }

        // GET: CriptomoedasHoje/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            var criptomoedaHoje = await _context.CriptomoedaHoje
                .Include(c => c.Criptomoeda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoedaHoje == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
            }

            return View(criptomoedaHoje);
        }

        // POST: CriptomoedasHoje/Delete/5
        [Authorize(Roles = "Admin")]
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

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

        public IActionResult Simulacao(string searchString, int? Busca)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "Bitcoin", Value = "1", Selected = true };
            SelectListItem item2 = new SelectListItem() { Text = "Ethereum", Value = "2", Selected = false };
            SelectListItem item3 = new SelectListItem() { Text = "Bitcoin Cash", Value = "3", Selected = false };
            SelectListItem item4 = new SelectListItem() { Text = "XRP", Value = "4", Selected = false };
            SelectListItem item5 = new SelectListItem() { Text = "PAX Gold", Value = "5", Selected = false };
            SelectListItem item6 = new SelectListItem() { Text = "Litecoin", Value = "6", Selected = false };
            itens.Add(item1);
            itens.Add(item2);
            itens.Add(item3);
            itens.Add(item4);
            itens.Add(item5);
            itens.Add(item6);

            ViewBag.Busca = itens;

            double valor = Convert.ToDouble(searchString);
            var valorHoje = 0.0;

            if (Busca != null)
            {
                itens.Where(i => i.Value == Busca.ToString()).First().Selected = true;
            }
            if (Busca == 1)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "Bitcoin" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    
                    var valorTotal = (valor/valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    } else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            if (Busca == 2)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "Ethereum" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    var valorTotal = (valor / valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    }
                    else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            if (Busca == 3)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "Bitcoin Cash" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    var valorTotal = (valor / valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    }
                    else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            if (Busca == 4)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "XRP" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    var valorTotal = (valor / valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    }
                    else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            if (Busca == 5)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "PAX Gold" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    var valorTotal = (valor / valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    }
                    else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            if (Busca == 6)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    valorHoje = (from c in _context.CriptomoedaHoje
                                 join cripto in _context.Criptomoeda
                                 on c.CriptomoedaId equals cripto.Id
                                 where cripto.Nome == "Litecoin" &&
                                 c.Data == DateTime.Today
                                 select c.Valor).Single();
                    var valorTotal = (valor / valorHoje).ToString("F6");

                    var valorTotalDouble = Convert.ToDouble(valorTotal);
                    if (valorTotalDouble >= 1)
                    {
                        ViewBag.Total = valorTotalDouble.ToString("F2");
                    }
                    else
                    {
                        ViewBag.Total = valorTotal;
                    }
                }
            }
            return View();
        }
    }
}
