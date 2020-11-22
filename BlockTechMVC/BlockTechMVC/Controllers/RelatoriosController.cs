using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlockTechMVC.Data;
using Microsoft.AspNetCore.Authorization;
using BlockTechMVC.Models;
using System.Diagnostics;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("relatorios")]
        // GET: Relatorios
        public ActionResult Index()
        {
            try { 
            ViewBag.Bitcoin = CriptomoedaHoje("Bitcoin").ToString("F4");
            ViewBag.Ethereum = CriptomoedaHoje("Ethereum").ToString("F4");
            ViewBag.BitcoinCash = CriptomoedaHoje("Bitcoin Cash").ToString("F4");
            ViewBag.XRP = CriptomoedaHoje("XRP").ToString("F4");
            ViewBag.PaxGold = CriptomoedaHoje("PAX Gold").ToString("F4");
            ViewBag.Litecoin = CriptomoedaHoje("Litecoin").ToString("F4");

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
                ViewBag.Dias = Ultimos7Dias();
                ViewBag.Valores = Valores7Dias("Bitcoin");

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
                ViewBag.Dias = Ultimos30Dias();
                ViewBag.Valores = Valores30Dias("Bitcoin");

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
            try { 
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Ethereum");

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
            try { 
            ViewBag.Dias = Ultimos30Dias();
            ViewBag.Valores = Valores30Dias("Ethereum");

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
            try { 
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Bitcoin Cash");

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
            try {
            ViewBag.Dias = Ultimos30Dias();
            ViewBag.Valores = Valores30Dias("Bitcoin Cash");

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
            try { 
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("XRP");

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
            try { 
            ViewBag.Dias = Ultimos30Dias();
            ViewBag.Valores = Valores30Dias("XRP");

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
            try { 
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("PAX Gold");

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
            try { 
            ViewBag.Dias = Ultimos30Dias();
            ViewBag.Valores = Valores30Dias("PAX Gold");

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
            try { 
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Litecoin");

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
            try { 
            ViewBag.Dias = Ultimos30Dias();
            ViewBag.Valores = Valores30Dias("Litecoin");

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
                ViewBag.Dias = Ultimos7Dias();
                ViewBag.Bitcoin = Porcentagem("Bitcoin");
                ViewBag.Ethereum = Porcentagem("Ethereum");
                ViewBag.BitcoinCash = Porcentagem("Bitcoin Cash");
                ViewBag.XRP = Porcentagem("XRP");
                ViewBag.PaxGold = Porcentagem("PAX Gold");
                ViewBag.Litecoin = Porcentagem("Litecoin");

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
                ViewBag.Dias = Ultimos30Dias();
                ViewBag.Bitcoin = PorcentagemTrinta("Bitcoin");
                ViewBag.Ethereum = PorcentagemTrinta("Ethereum");
                ViewBag.BitcoinCash = PorcentagemTrinta("Bitcoin Cash");
                ViewBag.XRP = PorcentagemTrinta("XRP");
                ViewBag.PaxGold = PorcentagemTrinta("PAX Gold");
                ViewBag.Litecoin = PorcentagemTrinta("Litecoin");
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        private double CriptomoedaHoje(string nome)
        {
            var criptomoeda = (from coin in _context.Criptomoeda
                               join criptohoje in _context.CriptomoedaHoje
                               on coin.Id equals criptohoje.CriptomoedaId
                               where coin.Nome == nome && criptohoje.Data == DateTime.Today
                               select criptohoje.Valor).Single();

            return criptomoeda;
        }

        private List<int> Ultimos7Dias()
        {
            var diasList = new List<int>();

            DateTime diasSete = DateTime.Today;

            for (int i = 7; i >= 0; i--)
            {
                diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                diasList.Add(diasSete.Day);
            }
            return diasList;
        }

        private List<int> Ultimos30Dias()
        {
            var diasList = new List<int>();

            DateTime diasTrinta = DateTime.Today;

            for (int i = 30; i >= 0; i--)
            {
                diasTrinta = DateTime.Today;
                diasTrinta = diasTrinta.AddDays(-i);
                diasList.Add(diasTrinta.Day);
            }
            return diasList;
        }

        private List<double> Valores7Dias(string nome)
        {
            var valorList = new List<double>();

            for (int i = 7; i >= 0; i--)
            {
                DateTime diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                DateTime data = diasSete;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();
                valorList.Add(valor);
            }
            return valorList;
        }

        private List<double> Valores30Dias(string nome)
        {
            var valorList = new List<double>();

            for (int i = 30; i >= 0; i--)
            {
                DateTime diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                DateTime data = diasSete;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();
                valorList.Add(valor);
            }

            return valorList;
        }

        public List<double> Porcentagem(string nome)
        {
            var date = DateTime.Today;
            date = date.AddDays(-8);

            var valor = (from coin in _context.Criptomoeda
                         join criptohoje in _context.CriptomoedaHoje
                         on coin.Id equals criptohoje.CriptomoedaId
                         where coin.Nome == nome && criptohoje.Data.Date.Equals(date.Date)
                         select criptohoje.Valor).Single();

            var calculo = 100.0;
            var porcentagem = new List<double>();

            for (int i = 7; i >= 0; i--)
            {
                DateTime dia = DateTime.Today;
                dia = dia.AddDays(-i);
                DateTime data = dia;

                var valorDia = (from coin in _context.Criptomoeda
                                join criptohoje in _context.CriptomoedaHoje
                                on coin.Id equals criptohoje.CriptomoedaId
                                where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                                select criptohoje.Valor).Single();

                var regra3 = ((valorDia * calculo) / valor);
                var resultado = regra3 - 100;
                valor = valorDia;

                var duasCasas = resultado.ToString("F2");
                var duasCasasDouble = Convert.ToDouble(duasCasas);
                porcentagem.Add(duasCasasDouble);
            }

            return porcentagem;
        }

        public List<double> PorcentagemTrinta(string nome)
        {
            var date = DateTime.Today;
            date = date.AddDays(-31);

            var valor = (from coin in _context.Criptomoeda
                         join criptohoje in _context.CriptomoedaHoje
                         on coin.Id equals criptohoje.CriptomoedaId
                         where coin.Nome == nome && criptohoje.Data.Date.Equals(date.Date)
                         select criptohoje.Valor).Single();

            var calculo = 100.0;
            var porcentagem = new List<double>();

            for (int i = 30; i >= 0; i--)
            {
                DateTime dia = DateTime.Today;
                dia = dia.AddDays(-i);
                DateTime data = dia;

                var valorDia = (from coin in _context.Criptomoeda
                                join criptohoje in _context.CriptomoedaHoje
                                on coin.Id equals criptohoje.CriptomoedaId
                                where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                                select criptohoje.Valor).Single();

                var regra3 = ((valorDia * calculo) / valor);
                var resultado = regra3 - 100;
                valor = valorDia;

                var duasCasas = resultado.ToString("F2");
                var duasCasasDouble = Convert.ToDouble(duasCasas);
                porcentagem.Add(duasCasasDouble);
            }

            return porcentagem;
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
