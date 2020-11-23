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

        [Route("transacoes/index")]
        // GET: Transacoes
        public async Task<IActionResult> Index(int? Busca, string searchString, string sortOrder)
        {
            var user = User.Identity.Name;
            try
            {
                ViewBag.Data = sortOrder == "Data" ? "Data_desc" : "Data";
                ViewBag.Quantidade = sortOrder == "Quantidade" ? "Quantidade_desc" : "Quantidade";
                ViewBag.Criptomoeda = sortOrder == "Criptomoeda" ? "Criptomoeda_desc" : "Criptomoeda";
                ViewBag.Valor = sortOrder == "Valor" ? "Valor_desc" : "Valor";

                if (user == "Administrador")
                {
                    ViewBag.Nome = sortOrder == "Nome" ? "Nome_desc" : "Nome";

                    List<SelectListItem> itens = new List<SelectListItem>();
                    SelectListItem item1 = new SelectListItem() { Text = "Cliente", Value = "1", Selected = true };
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
                        .Include(t => t.Saldo)
                        .OrderBy(t => t.Data);

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
                            case "Criptomoeda":
                                orderName = applicationDbContext.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Criptomoeda_desc":
                                orderName = applicationDbContext.OrderByDescending(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Valor":
                                orderName = applicationDbContext.OrderBy(s => s.Valor);
                                break;
                            case "Valor_desc":
                                orderName = applicationDbContext.OrderByDescending(s => s.Valor);
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
                    if (Busca == 1)
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
                            case "Valor":
                                orderName = usuario.OrderBy(s => s.Valor);
                                break;
                            case "Valor_desc":
                                orderName = usuario.OrderByDescending(s => s.Valor);
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
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
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
