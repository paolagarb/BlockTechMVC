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
using System.Web.Helpers;
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

        // GET: Transacoes
        public async Task<IActionResult> Index(int? Busca, string searchString)
        {
            var user = User.Identity.Name;

            if (user == "Administrador")
            {
                List<SelectListItem> itens = new List<SelectListItem>();
                SelectListItem item1 = new SelectListItem() { Text = "Cliente", Value = "1", Selected = true };
                SelectListItem item2 = new SelectListItem() { Text = "Criptomoeda", Value = "2", Selected = false };
                SelectListItem item3 = new SelectListItem() { Text = "Tipo de Transação", Value = "3", Selected = false };
                itens.Add(item1);
                itens.Add(item2);
                itens.Add(item3);

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
                    if (Busca == 3)
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
                                .Where(t => t.Tipo.Equals(searchString));

                            return View(usuarioSelecionado.ToList());
                        }
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

                return View(await usuario.ToListAsync());
            }
        }

        // GET: Transacoes/Details/5
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
