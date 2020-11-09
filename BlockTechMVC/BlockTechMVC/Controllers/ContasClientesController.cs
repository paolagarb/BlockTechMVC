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
                ViewBag.NameSortParm = sortOrder == "Nome" ? "Nome_desc" : "Nome";
                ViewBag.ValueSortParm = sortOrder == "Valor" ? "Valor_desc" : "Valor";

                var application = _context.Saldo
                    .Include(t => t.ContaCliente)
                    .Include(t => t.ContaCliente.ApplicationUser);

                if (sortOrder != null)
                {
                    var usuario = application.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);

                    switch (sortOrder)
                    {
                        case "Nome":
                            usuario = application.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Nome_desc":
                            usuario = application.OrderByDescending(c => c.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Valor_desc":
                            usuario = application.OrderByDescending(c => c.SaldoAtualRS);
                            break;
                        case "Valor":
                            usuario = application.OrderBy(c => c.SaldoAtualRS);
                            break;
                        default:
                            usuario = application.OrderByDescending(c => c.ContaCliente.ApplicationUser.Nome);
                            break;
                    }

                    return View(await usuario.ToListAsync());
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    var usuarioSelecionado = _context.Saldo
                   .Include(t => t.ContaCliente)
                   .Include(t => t.ContaCliente.ApplicationUser)
                   .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(searchString));

                    return View(usuarioSelecionado.ToList());
                }

                return View(await application.ToListAsync());
            }
            else
            {
                var usuario = _context.Saldo
                      .Include(t => t.ContaCliente)
                      .Include(t => t.ContaCliente.ApplicationUser)
                      .Where(t=>t.ContaCliente.ApplicationUser.UserName == user);

                return View(await usuario.ToListAsync());
            }
        }

        // GET: ContasClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

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
                .FirstOrDefaultAsync(m => m.Saldo.Id == id);

            if (transacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Transação não encontrada!" });
            }

            ViewData["ContaVinculada"] = "Conta Vinculada";
            return View(transacao);
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Aplicacoes(int? id, string searchString, int? Busca, string sortOrder)
        {
            var user = User.Identity.Name;

            ViewBag.Data = sortOrder == "Data" ? "Data_desc" : "Data";
            ViewBag.Quantidade = sortOrder == "Quantidade" ? "Quantidade_desc" : "Quantidade";
            ViewBag.Criptomoeda = sortOrder == "Criptomoeda" ? "Criptomoeda_desc" : "Criptomoeda";


            if (user == "Administrador")
            {

                ViewBag.Nome = sortOrder == "Nome" ? "Nome_desc" : "Nome";

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
          
                if (sortOrder != null)
                {
                    var orderName = applicationDbContext.OrderBy(t => t.ContaCliente.ApplicationUser.Nome);

                    switch (sortOrder)
                    {
                        case "Nome_desc":
                            orderName = orderName.OrderByDescending(s => s.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Nome":
                            orderName = applicationDbContext.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Data":
                            orderName = applicationDbContext.OrderBy(s => s.Data);
                            break;
                        case "Data_desc":
                            orderName = applicationDbContext.OrderByDescending(s => s.Data);
                            break;
                        case "Quantidade":
                            orderName = applicationDbContext.OrderBy(s => s.CriptoSaldo.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = applicationDbContext.OrderByDescending(s => s.CriptoSaldo.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = applicationDbContext.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                            break;
                        case "Criptomoeda_desc":
                            orderName = applicationDbContext.OrderByDescending(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                            break;
                        default:
                            orderName = applicationDbContext.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
                            break;
                    };
                    return View(orderName.ToList());
                }

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

                if (sortOrder != null)
                {
                    var orderName = usuario.OrderBy(t => t.CriptomoedaHoje.Criptomoeda.Nome);

                    switch (sortOrder)
                    {
                        case "Data":
                            orderName = usuario.OrderBy(s => s.Data);
                            break;
                        case "Data_desc":
                            orderName = usuario.OrderByDescending(s => s.Data);
                            break;
                        case "Quantidade":
                            orderName = usuario.OrderBy(s => s.CriptoSaldo.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = usuario.OrderByDescending(s => s.CriptoSaldo.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = usuario.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                            break;
                        case "Criptomoeda_desc":
                            orderName = usuario.OrderByDescending(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                            break;
                        default:
                            orderName = usuario.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                            break;
                    };
                    return View(orderName.ToList());
                }

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
