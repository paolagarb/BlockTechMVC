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
using System.Diagnostics;
using Microsoft.Extensions.Primitives;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class ContasClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContasClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContasClientes
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var user = User.Identity.Name;

            if (user == "Administrador")
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nome" : "";
                ViewBag.ValueSortParm = sortOrder == "Valor" ? "Valor_desc" : "Valor";

                var applicationDbContext = _context.Transacao
                    .Include(t => t.ContaCliente)
                    .Include(t => t.CriptoSaldo)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                    .Include(t => t.Saldo);
                if (sortOrder != null)
                {
                    var usuario = applicationDbContext.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);

                    switch (sortOrder)
                    {
                        case "Nome":
                            usuario = applicationDbContext.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Valor_desc":
                            usuario = applicationDbContext.OrderByDescending(c => c.Valor);
                            break;
                        case "Valor":
                            usuario = applicationDbContext.OrderBy(c => c.Valor);
                            break;
                        default:
                            usuario = applicationDbContext.OrderByDescending(c => c.ContaCliente.ApplicationUser.Nome);
                            break;
                    }

                    return View(await usuario.ToListAsync());
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    var usuarioSelecionado = _context.Transacao
                        .Include(t => t.ContaCliente)
                        .Include(t => t.CriptoSaldo)
                        .Include(t => t.CriptomoedaHoje)
                        .Include(t => t.ContaCliente.ApplicationUser)
                        .Include(t => t.CriptomoedaHoje.Criptomoeda)
                        .Include(t => t.Saldo)
                        .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(searchString));

                    return View(usuarioSelecionado.ToList());
                }

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var usuario = _context.Transacao
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.ContaCliente)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptoSaldo)
                    .Where(t => t.CriptoSaldo.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                    .Include(t => t.Saldo)
                    .Where(t => t.Saldo.ContaCliente.ApplicationUser.UserName == user);

                return View(await usuario.ToListAsync());
            }
        }

        // GET: ContasClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["ContaVinculada"] = "Conta Vinculada";

            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Transação não encontrada!" });
            }

            var transacao = await _context.Transacao
                .Include(t => t.ContaCliente)
                .Include(t => t.CriptoSaldo)
                .Include(t => t.CriptomoedaHoje)
                .Include(t => t.ContaCliente.Conta)
                .Include(t => t.ContaCliente.ApplicationUser)
                .Include(t => t.Saldo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Transação não encontrada!" });
            }

            return View(transacao);
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Aplicacoes(int? id, string searchString, int? Busca)
        {
            var user = User.Identity.Name;

            if (user == "Administrador")
            {
                List<SelectListItem> itens = new List<SelectListItem>();
                SelectListItem item1 = new SelectListItem() { Text = "Nome/Razão Social", Value = "1", Selected = true };
                SelectListItem item2 = new SelectListItem() { Text = "Criptomoeda", Value = "2", Selected = false };
                itens.Add(item1);
                itens.Add(item2);

                ViewBag.Busca = itens;

                var applicationDbContext = _context.Transacao
                    .Include(t => t.ContaCliente)
                    .Include(t => t.CriptoSaldo)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                    .Include(t => t.Saldo);

                if (Busca != null)
                {

                    itens.Where(i => i.Value == Busca.ToString()).First().Selected = true;
                }
                    if (Busca==1)
                    {

                        if (!String.IsNullOrEmpty(searchString))
                        {
                            var usuarioSelecionado = _context.Transacao
                                .Include(t => t.ContaCliente)
                                .Include(t => t.CriptoSaldo)
                                .Include(t => t.CriptomoedaHoje)
                                .Include(t => t.ContaCliente.ApplicationUser)
                                .Include(t => t.CriptomoedaHoje.Criptomoeda)
                                .Include(t => t.Saldo)
                                .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(searchString));

                            return View(usuarioSelecionado.ToList());
                        }
                    }
                    if (Busca == 2)
                    {
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            var usuarioSelecionado = _context.Transacao
                                .Include(t => t.ContaCliente)
                                .Include(t => t.CriptoSaldo)
                                .Include(t => t.CriptomoedaHoje)
                                .Include(t => t.ContaCliente.ApplicationUser)
                                .Include(t => t.CriptomoedaHoje.Criptomoeda)
                                .Include(t => t.Saldo)
                                .Where(t => t.CriptomoedaHoje.Criptomoeda.Nome.Contains(searchString));

                            return View(usuarioSelecionado.ToList());
                        }
                    }

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var usuario = _context.Transacao
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.ContaCliente)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptoSaldo)
                    .Where(t => t.CriptoSaldo.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                    .Include(t => t.Saldo)
                    .Where(t => t.Saldo.ContaCliente.ApplicationUser.UserName == user);

                if (!String.IsNullOrEmpty(searchString))
                {
                    var criptoSelecionada = _context.Transacao
                        .Include(t => t.ContaCliente)
                        .Include(t => t.CriptoSaldo)
                        .Include(t => t.CriptomoedaHoje)
                        .Include(t => t.ContaCliente.ApplicationUser)
                        .Include(t => t.CriptomoedaHoje.Criptomoeda)
                        .Include(t => t.Saldo)
                        .Where(t => t.CriptomoedaHoje.Criptomoeda.Nome.Contains(searchString) && t.Saldo.ContaCliente.ApplicationUser.UserName == user);

                    return View(criptoSelecionada.ToList());
                }

                return View(await usuario.ToListAsync());
            }
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
    }
}
