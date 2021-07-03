using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class ContasClientesController : Controller
    {
        private readonly IContaRepository _contaRepository;
        private readonly ICalculoRepository _calculoRepository;

        public ContasClientesController(IContaRepository contaRepository, ICalculoRepository calculoRepository)
        {
            _contaRepository = contaRepository;
            _calculoRepository = calculoRepository;
        }

        [Route("conta")]
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            try
            {
                var user = User?.Identity?.Name;

                ViewBag.NameSortParm = sortOrder == "Nome" ? "Nome_desc" : "Nome";
                ViewBag.Total = SaldoTotal(user);

                if (user != "Administrador")
                {
                    var usuario = _contaRepository.Listar(user);

                    return View(usuario);
                }

                List<string> usuarios = _contaRepository.ListarUsuarios();
                List<double> saldoUsuarios = new List<double>();

                foreach (var item in usuarios)
                {
                    saldoUsuarios.Add(SaldoTotal(item));
                }

                ViewBag.TotalAdmin = saldoUsuarios;
                var aplicacoes = _contaRepository.CarregarAplicacoes();

                if (!String.IsNullOrEmpty(sortOrder))
                {
                    var usuario = aplicacoes.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);

                    if (sortOrder.Equals("Nome"))
                        usuario = aplicacoes.OrderBy(c => c.ContaCliente.ApplicationUser.Nome);
                    else
                        usuario = aplicacoes.OrderByDescending(c => c.ContaCliente.ApplicationUser.Nome);

                    return View(await usuario.ToListAsync());
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    var selecionado = _contaRepository.ListarContains(searchString);

                    return View(selecionado.ToList());
                }

                return View(await aplicacoes.ToListAsync());

            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("conta/detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                    return RedirectToAction(nameof(Error), new { message = "Transação não encontrada!" });

                var user = User?.Identity?.Name;
                var usuario = _contaRepository.CarregarUsuario(id);

                ViewBag.TotalAdm = SaldoTotal(usuario).ToString("F2");
                ViewBag.Total = SaldoTotal(user).ToString("F2");

                var transacao = _contaRepository.CarregarTransacao(id);
                if (transacao == null)
                    return RedirectToAction(nameof(Error), new { message = "Transação não encontrada!" });

                return View(transacao);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        public double SaldoTotal(string user)
        {
            var saldoSemInvestimento = _calculoRepository.TotalNaoInvestido(user);

            double bitcoin = _calculoRepository.TotalCriptomoeda("Bitcoin", user);
            double saldoTotalBitcoin = _calculoRepository.SaldoAtual(bitcoin, "Bitcoin");

            double ethereum = _calculoRepository.TotalCriptomoeda("Ethereum", user);
            double saldoTotalEthereum = _calculoRepository.SaldoAtual(ethereum, "Ethereum");

            double bitcoinCash = _calculoRepository.TotalCriptomoeda("Bitcoin Cash", user);
            double saldoTotalBitcoinCash = _calculoRepository.SaldoAtual(bitcoinCash, "Bitcoin Cash");

            double xrp = _calculoRepository.TotalCriptomoeda("XRP", user);
            double saldoTotalXrp = _calculoRepository.SaldoAtual(xrp, "XRP");

            double paxGold = _calculoRepository.TotalCriptomoeda("PAX Gold", user);
            double saldoTotalPaxGold = _calculoRepository.SaldoAtual(paxGold, "PAX Gold");

            double litecoin = _calculoRepository.TotalCriptomoeda("Litecoin", user);
            double saldoTotalLitecoin = _calculoRepository.SaldoAtual(litecoin, "Litecoin");

            var total = (saldoSemInvestimento +
                        saldoTotalBitcoin +
                        saldoTotalEthereum +
                        saldoTotalBitcoinCash +
                        saldoTotalXrp +
                        saldoTotalPaxGold +
                        saldoTotalLitecoin);

            return total;
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
