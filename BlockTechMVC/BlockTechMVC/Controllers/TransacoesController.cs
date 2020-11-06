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
        public async Task<IActionResult> Index()
        {
            var user = User.Identity.Name;

            if (user == "Administrador")
            {
                var applicationDbContext = _context.Transacao
                    .Include(t => t.ContaCliente)
                    .Include(t => t.CriptoSaldo)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Include(t => t.CriptomoedaHoje.Criptomoeda)
                    .Include(t => t.Saldo);

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
