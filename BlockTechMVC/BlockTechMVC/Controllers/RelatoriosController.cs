using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        private readonly IRelatorioRepository _repository;

        public RelatoriosController(IRelatorioRepository repository)
        {
            _repository = repository;
        }

        [Route("relatorios")]
        public ActionResult Index()
        {
            try
            {
                ViewBag.Bitcoin = _repository.ValorAtualCriptomoeda("Bitcoin").ToString("F4");
                ViewBag.Ethereum = _repository.ValorAtualCriptomoeda("Ethereum").ToString("F4");
                ViewBag.BitcoinCash = _repository.ValorAtualCriptomoeda("Bitcoin Cash").ToString("F4");
                ViewBag.XRP = _repository.ValorAtualCriptomoeda("XRP").ToString("F4");
                ViewBag.PaxGold = _repository.ValorAtualCriptomoeda("PAX Gold").ToString("F4");
                ViewBag.Litecoin = _repository.ValorAtualCriptomoeda("Litecoin").ToString("F4");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/bitcoin")]
        public ActionResult Bitcoin()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "Bitcoin");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/bitcoin-mes")]
        public ActionResult Bitcoin30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "Bitcoin");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/ethereum")]
        public ActionResult Ethereum()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "Ethereum");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/ethereum-mes")]
        public ActionResult Ethereum30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "Ethereum");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/bitcoin-cash")]
        public ActionResult BitcoinCash()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "Bitcoin Cash");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/bitcoin-cash-mes")]
        public ActionResult BitcoinCash30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "Bitcoin Cash");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/xrp")]
        public ActionResult Xrp()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "XRP");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/xrp-mes")]
        public ActionResult Xrp30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "XRP");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/pax-gold")]
        public ActionResult PaxGold()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "PAX Gold");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/pax-gold-mes")]
        public ActionResult PaxGold30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "PAX Gold");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/litecoin")]
        public ActionResult Litecoin()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Valores = _repository.SaldoUltimosDias(7, "Litecoin");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/litecoin-mes")]
        public ActionResult Litecoin30()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Valores = _repository.SaldoUltimosDias(30, "Litecoin");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/semanal")]
        public IActionResult Semanal()
        {
            try
            {
                ViewBag.Dias = UltimosDias(7);
                ViewBag.Bitcoin = _repository.PorcentualUltimosDias(7, "Bitcoin");
                ViewBag.Ethereum = _repository.PorcentualUltimosDias(7, "Ethereum");
                ViewBag.BitcoinCash = _repository.PorcentualUltimosDias(7, "Bitcoin Cash");
                ViewBag.XRP = _repository.PorcentualUltimosDias(7, "XRP");
                ViewBag.PaxGold = _repository.PorcentualUltimosDias(7, "PAX Gold");
                ViewBag.Litecoin = _repository.PorcentualUltimosDias(7, "Litecoin");

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("relatorios/mensal")]
        public IActionResult Mensal()
        {
            try
            {
                ViewBag.Dias = UltimosDias(30);
                ViewBag.Bitcoin = _repository.PorcentualUltimosDias(30, "Bitcoin");
                ViewBag.Ethereum = _repository.PorcentualUltimosDias(30, "Ethereum");
                ViewBag.BitcoinCash = _repository.PorcentualUltimosDias(30, "Bitcoin Cash");
                ViewBag.XRP = _repository.PorcentualUltimosDias(30, "XRP");
                ViewBag.PaxGold = _repository.PorcentualUltimosDias(30, "PAX Gold");
                ViewBag.Litecoin = _repository.PorcentualUltimosDias(30, "Litecoin");
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        private List<int> UltimosDias(int quantidadeDias)
        {
            var list = new List<int>();

            for (int i = quantidadeDias; i >= 0; i--)
            {
                DateTime dias = DateTime.Today;
                dias = dias.AddDays(-i);
                list.Add(dias.Day);
            }

            return list;
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
