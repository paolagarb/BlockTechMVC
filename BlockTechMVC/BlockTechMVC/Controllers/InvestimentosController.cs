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
    public class InvestimentosController : Controller
    {
        private readonly IInvestimentoRepository _investimentoRepository;
        private readonly ICalculoRepository _calculoRepository;

        public InvestimentosController(IInvestimentoRepository investimentoRepository, ICalculoRepository calculoRepository)
        {
            _investimentoRepository = investimentoRepository;
            _calculoRepository = calculoRepository;
        }

        [Route("investimentos")]
        // GET: MeusInvestimentos
        public IActionResult Index()
        {
            var user = User.Identity.Name;
            try
            {
                if (user == "Administrador")
                {
                    var bitcoin = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin");
                    ViewBag.Bitcoin = bitcoin;
                    ViewBag.BitcoinValorRS = _investimentoRepository.ValorInvestidoAdm("Bitcoin").ToString("F2"); ;

                    var ethereum = _investimentoRepository.QuantidadeCriptomoedaAdm("Ethereum");
                    ViewBag.Ethereum = ethereum;
                    ViewBag.EthereumValorRS = _investimentoRepository.ValorInvestidoAdm("Ethereum").ToString("F2");

                    var bitcoinCash = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin Cash");
                    ViewBag.BitcoinCash = bitcoinCash;
                    ViewBag.BitcoinCashValorRS = _investimentoRepository.ValorInvestidoAdm("Bitcoin Cash").ToString("F2");

                    var litecoin = _investimentoRepository.QuantidadeCriptomoedaAdm("Litecoin");
                    ViewBag.Litecoin = litecoin;
                    ViewBag.LitecoinValorRS = _investimentoRepository.ValorInvestidoAdm("Litecoin").ToString("F2");

                    var paxGold = _investimentoRepository.QuantidadeCriptomoedaAdm("PAX Gold");
                    ViewBag.PaxGold = paxGold;
                    ViewBag.PaxGoldValorRS = _investimentoRepository.ValorInvestidoAdm("PAX Gold").ToString("F2");

                    var xrp = _investimentoRepository.QuantidadeCriptomoedaAdm("XRP");
                    ViewBag.Xrp = xrp;
                    ViewBag.XrpValorRS = _investimentoRepository.ValorInvestidoAdm("XRP").ToString("F2");

                    return View();
                }
                else
                {
                    double bitcoin = _calculoRepository.TotalCriptomoeda("Bitcoin", user);
                    ViewBag.QuantidadaTotalBitcoin = bitcoin.ToString("F6");

                    double saldoTotalBitcoin = _calculoRepository.SaldoAtual(bitcoin, "Bitcoin");
                    ViewBag.QuantidadaEmRealBitcoin = saldoTotalBitcoin.ToString("F2");

                    double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                    ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                    double ethereum = _calculoRepository.TotalCriptomoeda("Ethereum", user);
                    ViewBag.QuantidadaTotalEthereum = ethereum.ToString("F6");

                    double saldoTotalEthereum = _calculoRepository.SaldoAtual(ethereum, "Ethereum");
                    ViewBag.QuantidadaEmRealEthereum = saldoTotalEthereum.ToString("F2");

                    double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                    ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                    double bitcoinCash = _calculoRepository.TotalCriptomoeda("Bitcoin Cash", user);
                    ViewBag.QuantidadaTotalBitcoinCash = bitcoinCash.ToString("F6");

                    double saldoTotalBitcoinCash = _calculoRepository.SaldoAtual(bitcoinCash, "Bitcoin Cash");
                    ViewBag.QuantidadaEmRealBitcoinCash = saldoTotalBitcoinCash.ToString("F2");

                    double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                    ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                    double xrp = _calculoRepository.TotalCriptomoeda("XRP", user);
                    ViewBag.QuantidadaTotalXrp = xrp.ToString("F6");

                    double saldoTotalXrp = _calculoRepository.SaldoAtual(xrp, "XRP");
                    ViewBag.QuantidadaEmRealXrp = saldoTotalXrp.ToString("F2");

                    double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                    ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                    double paxGold = _calculoRepository.TotalCriptomoeda("PAX Gold", user);
                    ViewBag.QuantidadaTotalPaxGold = paxGold.ToString("F6");

                    double saldoTotalPaxGold = _calculoRepository.SaldoAtual(paxGold, "PAX Gold");
                    ViewBag.QuantidadaEmRealPaxGold = saldoTotalPaxGold.ToString("F2");

                    double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                    ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                    double litecoin = _calculoRepository.TotalCriptomoeda("Litecoin", user);
                    ViewBag.QuantidadaTotalLitecoin = litecoin.ToString("F6");

                    double saldoTotalLitecoin = _calculoRepository.SaldoAtual(litecoin, "Litecoin");
                    ViewBag.QuantidadaEmRealLitecoin = saldoTotalLitecoin.ToString("F2");

                    double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                    ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                    double saldoSemInvestimento = _calculoRepository.TotalNaoInvestido(user);
                    ViewBag.SaldoSemInvestimento = saldoSemInvestimento.ToString("F2");

                    double dinheiroTotalConta = saldoSemInvestimento + saldoTotalBitcoin + saldoTotalEthereum + saldoTotalBitcoinCash + saldoTotalXrp + saldoTotalPaxGold + saldoTotalLitecoin;
                    ViewBag.DinheiroTotalContaRS = dinheiroTotalConta.ToString("F2");

                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/bitcoin")]
        public ActionResult Bitcoin()
        {
            var user = User.Identity.Name;

            try
            {
                double bitcoin = _calculoRepository.TotalCriptomoeda("Bitcoin", user);
                ViewBag.QuantidadaTotalBitcoin = bitcoin.ToString("F6");

                double saldoTotalBitcoin = _calculoRepository.SaldoAtual(bitcoin, "Bitcoin");
                ViewBag.QuantidadaEmRealBitcoin = saldoTotalBitcoin.ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalBitcoin - valorInvestidoBitcoin).ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var bitcoinAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin");
                ViewBag.Bitcoin = bitcoinAdm;
                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "Bitcoin", bitcoinAdm);

                var BitcoinValorRSString = _investimentoRepository.ValorCriptomoedaAdm("Bitcoin", bitcoinAdm).ToString("F2");
                var BitcoinValorRS = Convert.ToDouble(BitcoinValorRSString);
                ViewBag.BitcoinValorRS = BitcoinValorRS;

                var BitcoinInvestidoString = _investimentoRepository.ValorInvestidoAdm("Bitcoin").ToString("F2");
                var BitcoinInvestido = Convert.ToDouble(BitcoinInvestidoString);
                ViewBag.BitcoinInvestido = BitcoinInvestido;

                ViewBag.LucroOuPerdaAdm = (BitcoinValorRS - BitcoinInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("Bitcoin", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorBitcoin7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, bitcoin, "Bitcoin");
                }
                else
                {
                    ViewBag.ValorBitcoin7Dias = _investimentoRepository.ValorTotalUltimosDias(7, bitcoin, "Bitcoin");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorBitcoinMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, bitcoin, "Bitcoin");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorBitcoinMes = _investimentoRepository.ValorTotalUltimosDias(31, bitcoin, "Bitcoin");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorBitcoinMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimentoGeralAdm, bitcoinAdm, "Bitcoin");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorBitcoinMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, bitcoinAdm, "Bitcoin");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/ethereum")]
        public ActionResult Ethereum()
        {
            var user = User.Identity.Name;
            try
            {
                double ethereum = _calculoRepository.TotalCriptomoeda("Ethereum", user);
                ViewBag.QuantidadaTotalEthereum = ethereum.ToString("F6");

                double saldoTotalEthereum = _calculoRepository.SaldoAtual(ethereum, "Ethereum");
                ViewBag.QuantidadaEmRealEthereum = saldoTotalEthereum.ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalEthereum - valorInvestidoEthereum).ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var ethereumAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Ethereum");
                ViewBag.Ethereum = ethereumAdm;

                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "Ethereum", ethereumAdm);

                var EthereumValorRString = _investimentoRepository.ValorCriptomoedaAdm("Ethereum", ethereumAdm).ToString("F2");
                double EthereumValorRS = Convert.ToDouble(EthereumValorRString);
                ViewBag.EthereumValorRS = EthereumValorRS;

                var EthereumInvestidoString = _investimentoRepository.ValorInvestidoAdm("Ethereum").ToString("F2");
                double EthereumInvestido = Convert.ToDouble(EthereumInvestidoString);
                ViewBag.EthereumInvestido = EthereumInvestido;

                ViewBag.LucroOuPerdaAdm = (EthereumValorRS - EthereumInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("Ethereum", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorEthereum7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, ethereum, "Ethereum");
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorEthereum7Dias = _investimentoRepository.ValorTotalUltimosDias(7, ethereum, "Ethereum");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, ethereum, "Ethereum");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(31, ethereum, "Ethereum");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Ethereum");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimentoGeralAdm, ethereumAdm, "Ethereum");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, ethereumAdm, "Ethereum");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/bitcoin-cash")]
        public ActionResult BitcoinCash()
        {
            var user = User.Identity.Name;
            try
            {
                double bitcoinCash = _calculoRepository.TotalCriptomoeda("Bitcoin Cash", user);
                ViewBag.QuantidadaTotalBitcoinCash = bitcoinCash.ToString("F6");

                double saldoTotalBitcoinCash = _calculoRepository.SaldoAtual(bitcoinCash, "Bitcoin Cash");
                ViewBag.QuantidadaEmRealBitcoinCash = saldoTotalBitcoinCash.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalBitcoinCash - valorInvestidoBitcoinCash).ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var bitcoinCashAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin Cash");
                ViewBag.BitcoinCash = bitcoinCashAdm;
                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "Bitcoin Cash", bitcoinCashAdm);

                var bitcoinCashValorRSString = _investimentoRepository.ValorCriptomoedaAdm("Bitcoin Cash", bitcoinCashAdm).ToString("F2");
                double bitcoinCashValorRS = Convert.ToDouble(bitcoinCashValorRSString);
                ViewBag.BitcoinCashValorRS = bitcoinCashValorRS;

                var BitcoinCashInvestidoString = _investimentoRepository.ValorInvestidoAdm("Bitcoin Cash").ToString("F2");
                double BitcoinCashInvestido = Convert.ToDouble(BitcoinCashInvestidoString);
                ViewBag.BitcoinCashInvestido = BitcoinCashInvestido;

                ViewBag.LucroOuPerdaAdm = (bitcoinCashValorRS - BitcoinCashInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("Bitcoin Cash", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorBitcoinCash7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, bitcoinCash, "Bitcoin Cash");
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorBitcoinCash7Dias = _investimentoRepository.ValorTotalUltimosDias(7, bitcoinCash, "Bitcoin Cash");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, bitcoinCash, "Bitcoin Cash");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(31, bitcoinCash, "Bitcoin Cash");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Bitcoin Cash");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimentoGeralAdm, bitcoinCashAdm, "Bitcoin Cash");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, bitcoinCashAdm, "Bitcoin Cash");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/xrp")]
        public ActionResult Xrp()
        {
            var user = User.Identity.Name;

            try
            {
                double xrp = _calculoRepository.TotalCriptomoeda("XRP", user);
                ViewBag.QuantidadaTotalXrp = xrp.ToString("F6");

                double saldoTotalXrp = _calculoRepository.SaldoAtual(xrp, "XRP");
                ViewBag.QuantidadaEmRealXrp = saldoTotalXrp.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalXrp - valorInvestidoXrp).ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var xrpAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("XRP");
                ViewBag.Xrp = xrpAdm;
                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "XRP", xrpAdm);

                var xrpValorRSString = _investimentoRepository.ValorCriptomoedaAdm("XRP", xrpAdm).ToString("F2");
                var xrpValorRS = Convert.ToDouble(xrpValorRSString);
                ViewBag.XrpValorRS = xrpValorRS;

                var xrpInvestidoString = _investimentoRepository.ValorInvestidoAdm("XRP").ToString("F2");
                var xrpInvestido = Convert.ToDouble(xrpInvestidoString);
                ViewBag.XrpInvestido = xrpInvestido;

                ViewBag.LucroOuPerdaAdm = (xrpValorRS - xrpInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("XRP", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorXrp7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, xrp, "XRP");
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorXrp7Dias = _investimentoRepository.ValorTotalUltimosDias(7, xrp, "XRP");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, xrp, "XRP");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(31, xrp, "XRP");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("XRP");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimentoGeralAdm, xrpAdm, "XRP");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, xrpAdm, "XRP");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/pax-gold")]
        public ActionResult PaxGold()
        {
            var user = User.Identity.Name;

            try
            {
                double paxGold = _calculoRepository.TotalCriptomoeda("PAX Gold", user);
                ViewBag.QuantidadaTotalPaxGold = paxGold.ToString("F6");

                double saldoTotalPaxGold = _calculoRepository.SaldoAtual(paxGold, "PAX Gold");
                ViewBag.QuantidadaEmRealPaxGold = saldoTotalPaxGold.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalPaxGold - valorInvestidoPaxGold).ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var PaxGold = _investimentoRepository.QuantidadeCriptomoedaAdm("PAX Gold");
                ViewBag.PaxGold = PaxGold;
                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "PAX Gold", PaxGold);

                var PaxGoldValorRSString = _investimentoRepository.ValorCriptomoedaAdm("PAX Gold", PaxGold).ToString("F2");
                double PaxGoldValorRS = Convert.ToDouble(PaxGoldValorRSString);
                ViewBag.PaxGoldValorRS = PaxGoldValorRS;

                var PaxGoldInvestidoString = _investimentoRepository.ValorInvestidoAdm("PAX Gold").ToString("F2");
                double PaxGoldInvestido = Convert.ToDouble(PaxGoldInvestidoString);
                ViewBag.PaxGoldInvestido = PaxGoldInvestido;

                ViewBag.LucroOuPerdaAdm = (PaxGoldValorRS - PaxGoldInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("PAX Gold", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorPaxGold7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, paxGold, "PAX Gold");
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorPaxGold7Dias = _investimentoRepository.ValorTotalUltimosDias(7, paxGold, "PAX Gold");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, paxGold, "PAX Gold");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(31, paxGold, "PAX Gold");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("PAX Gold");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimentoGeralAdm, PaxGold, "PAX Gold");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, PaxGold, "PAX Gold");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        [Route("investimentos/litecoin")]
        public ActionResult Litecoin()
        {
            var user = User.Identity.Name;

            try
            {
                double litecoin = _calculoRepository.TotalCriptomoeda("Litecoin", user);
                ViewBag.QuantidadaTotalLitecoin = litecoin.ToString("F6");

                double saldoTotalLitecoin = _calculoRepository.SaldoAtual(litecoin, "Litecoin");
                ViewBag.QuantidadaEmRealLitecoin = saldoTotalLitecoin.ToString("F2");

                double valorInvestidoLitecoin = _investimentoRepository.ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalLitecoin - valorInvestidoLitecoin).ToString("F2");

                double valorInvestidoBitcoin = _investimentoRepository.ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = _investimentoRepository.ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = _investimentoRepository.ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = _investimentoRepository.ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = _investimentoRepository.ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                var Litecoin = _investimentoRepository.QuantidadeCriptomoedaAdm("Litecoin");
                ViewBag.Litecoin = Litecoin;
                ViewBag.Ultimos7DiasAdm = _investimentoRepository.ValorTotalUltimosDiasAdm(7, "Litecoin", Litecoin);

                var LitecoinValorRSString = _investimentoRepository.ValorCriptomoedaAdm("Litecoin", Litecoin).ToString("F2");
                var LitecoinValorRS = Convert.ToDouble(LitecoinValorRSString);
                ViewBag.LitecoinValorRS = LitecoinValorRS;

                var LitecoinInvestidoString = _investimentoRepository.ValorInvestidoAdm("Litecoin").ToString("F2");
                var LitecoinInvestido = Convert.ToDouble(LitecoinInvestidoString);
                ViewBag.LitecoinInvestido = LitecoinInvestido;

                ViewBag.LucroOuPerdaAdm = (LitecoinValorRS - LitecoinInvestido).ToString("F2");

                int primeiroInvestimento = _investimentoRepository.PrimeiroInvestimento("Litecoin", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorLitecoin7Dias = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, litecoin, "Litecoin");
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorLitecoin7Dias = _investimentoRepository.ValorTotalUltimosDias(7, litecoin, "Litecoin");
                    ViewBag.Ultimos7Dias = UltimosDias(6);
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, litecoin, "Litecoin");
                }
                else
                {
                    ViewBag.UltimoMes = UltimosDias(31);
                    ViewBag.ValorMes = _investimentoRepository.ValorTotalUltimosDias(31, litecoin, "Litecoin");
                }

                int primeiroInvestimentoGeralAdm = _investimentoRepository.QuantidadeCriptomoedaAdm("Litecoin");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(primeiroInvestimento, Litecoin, "Litecoin");
                }
                else
                {
                    ViewBag.UltimoMesAdm = UltimosDias(31);
                    ViewBag.ValorMesAdm = _investimentoRepository.ValorTotalUltimosDias(31, Litecoin, "Litecoin");
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        public List<int> UltimosDias(int dias)
        {
            var list = new List<int>();

            for (int i = dias; i >= 0; i--)
            {
                DateTime data = DateTime.Today;
                data = data.AddDays(-i);
                list.Add(data.Day);
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
