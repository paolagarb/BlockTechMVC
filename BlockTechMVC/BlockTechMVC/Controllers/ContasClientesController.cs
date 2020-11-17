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

            ViewBag.Total = SaldoTotal(user);

            if (user == "Administrador")
            {
                double[] usuarios = new double[20];
                int i = 0;
                foreach (var item in _context.Saldo)
                {
                    var usuario = (from c in _context.Saldo
                                   join conta in _context.ContaCliente
                                   on c.ContaClienteId equals conta.Id
                                   join cliente in _context.ApplicationUser
                                   on conta.ApplicationUserID equals cliente.Id
                                   where item.ContaClienteId == conta.Id
                                   select cliente.UserName).FirstOrDefault();
                    var saldo = SaldoTotal(usuario);

                    usuarios[i] = saldo;
                    i++;
                }
                ViewBag.TotalAdm = usuarios.ToList();
            }

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
                      .Where(t => t.ContaCliente.ApplicationUser.UserName == user);

                return View(await usuario.ToListAsync());
            }
        }

        // GET: ContasClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.Identity.Name;


            var usuario = (from c in _context.Saldo
                           join conta in _context.ContaCliente
                           on c.ContaClienteId equals conta.Id
                           join cliente in _context.ApplicationUser
                           on conta.ApplicationUserID equals cliente.Id
                           where c.Id == id
                           select cliente.UserName).FirstOrDefault();
            ViewBag.TotalAdm = SaldoTotal(usuario);


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
            ViewBag.Total = SaldoTotal(user);

            return View(transacao);
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Aplicacoes(int? id, string searchString, int? Busca, string sortOrder)
        {
            var user = User.Identity.Name;

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

                //var applicationDbContext = _context.Transacao
                //    .Include(t => t.ContaCliente)
                //    .Include(t => t.CriptoSaldo)
                //    .Include(t => t.CriptomoedaHoje)
                //    .Include(t => t.ContaCliente.ApplicationUser)
                //    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                //    .Include(t => t.Saldo);
                var applicationDbContext = _context.CriptoSaldo
                    .Include(t => t.ContaCliente)
                    .Include(t => t.ContaCliente.ApplicationUser);

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
                        case "Quantidade":
                            orderName = applicationDbContext.OrderBy(s => s.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = applicationDbContext.OrderByDescending(s => s.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = applicationDbContext.OrderBy(s => s.Criptomoeda);
                            break;
                        case "Criptomoeda_desc":
                            orderName = applicationDbContext.OrderByDescending(s => s.Criptomoeda);
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
                        var usuarioSelecionado = _context.CriptoSaldo
                        .Include(t => t.ContaCliente)
                        .Include(t => t.ContaCliente.ApplicationUser)
                        .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(searchString));

                        return View(usuarioSelecionado.ToList());
                    }
                }
                if (Busca == 2)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        var usuarioSelecionado = _context.CriptoSaldo
                       .Include(t => t.ContaCliente)
                       .Include(t => t.ContaCliente.ApplicationUser)
                       .Where(t => t.Criptomoeda.Contains(searchString));

                        return View(usuarioSelecionado.ToList());
                    }
                }

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var usuario = _context.CriptoSaldo
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.ContaCliente)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user);

                if (sortOrder != null)
                {
                    var orderName = usuario.OrderBy(t => t.Criptomoeda);

                    switch (sortOrder)
                    {
                        case "Quantidade":
                            orderName = usuario.OrderBy(s => s.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = usuario.OrderByDescending(s => s.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = usuario.OrderBy(s => s.Criptomoeda);
                            break;
                        case "Criptomoeda_desc":
                            orderName = usuario.OrderByDescending(s => s.Criptomoeda);
                            break;
                        default:
                            orderName = usuario.OrderBy(s => s.Criptomoeda);
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

        public double SaldoTotal(string user)
        {

            MeusInvestimentosController investimentos = new MeusInvestimentosController(_context);
            var saldoSemInvestimento = investimentos.SaldoSemInvestimento(user);
            double bitcoin = investimentos.QuantidadeTotalCriptomoeda("Bitcoin", user);
            double saldoTotalBitcoin = investimentos.CalcularSaldoAtual(bitcoin, "Bitcoin");
            double ethereum = investimentos.QuantidadeTotalCriptomoeda("Ethereum", user);
            double saldoTotalEthereum = investimentos.CalcularSaldoAtual(ethereum, "Ethereum");
            double bitcoinCash = investimentos.QuantidadeTotalCriptomoeda("Bitcoin Cash", user);
            double saldoTotalBitcoinCash = investimentos.CalcularSaldoAtual(bitcoinCash, "Bitcoin Cash");
            double xrp = investimentos.QuantidadeTotalCriptomoeda("XRP", user);
            double saldoTotalXrp = investimentos.CalcularSaldoAtual(xrp, "XRP");
            double paxGold = investimentos.QuantidadeTotalCriptomoeda("PAX Gold", user);
            double saldoTotalPaxGold = investimentos.CalcularSaldoAtual(paxGold, "PAX Gold");
            double litecoin = investimentos.QuantidadeTotalCriptomoeda("Litecoin", user);
            double saldoTotalLitecoin = investimentos.CalcularSaldoAtual(litecoin, "Litecoin");

            var total = (saldoSemInvestimento + saldoTotalBitcoin + saldoTotalEthereum + saldoTotalBitcoinCash + saldoTotalXrp + saldoTotalPaxGold + saldoTotalLitecoin).ToString("F2");
            ViewBag.Total = total;

            return Convert.ToDouble(total);
        }
    }
}
