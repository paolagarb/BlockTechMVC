using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlockTechMVC.Data;
using BlockTechMVC.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using BlockTechMVC.Models;
using System.Diagnostics;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class MeusInvestimentosController : CoreController
    {
        private readonly ApplicationDbContext _context;

        public MeusInvestimentosController(ApplicationDbContext context) : base(context)
        {
            _context = context;
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
                    var Bitcoin = QuantidadeTotalCriptomoedaAdm("Bitcoin");
                    ViewBag.Bitcoin = Bitcoin;
                    ViewBag.BitcoinValorRS = ValorInvestidoAdm("Bitcoin").ToString("F2"); ;

                    var Ethereum = QuantidadeTotalCriptomoedaAdm("Ethereum");
                    ViewBag.Ethereum = Ethereum;
                    ViewBag.EthereumValorRS = ValorInvestidoAdm("Ethereum").ToString("F2");

                    var BitcoinCash = QuantidadeTotalCriptomoedaAdm("Bitcoin Cash");
                    ViewBag.BitcoinCash = BitcoinCash;
                    ViewBag.BitcoinCashValorRS = ValorInvestidoAdm("Bitcoin Cash").ToString("F2");

                    var Litecoin = QuantidadeTotalCriptomoedaAdm("Litecoin");
                    ViewBag.Litecoin = Litecoin;
                    ViewBag.LitecoinValorRS = ValorInvestidoAdm("Litecoin").ToString("F2");

                    var PaxGold = QuantidadeTotalCriptomoedaAdm("PAX Gold");
                    ViewBag.PaxGold = PaxGold;
                    ViewBag.PaxGoldValorRS = ValorInvestidoAdm("PAX Gold").ToString("F2");

                    var Xrp = QuantidadeTotalCriptomoedaAdm("XRP");
                    ViewBag.Xrp = Xrp;
                    ViewBag.XrpValorRS = ValorInvestidoAdm("XRP").ToString("F2");

                    return View();
                }
                else
                {
                    double bitcoin = QuantidadeTotalCriptomoeda("Bitcoin", user);
                    ViewBag.QuantidadaTotalBitcoin = bitcoin.ToString("F6");

                    double saldoTotalBitcoin = CalcularSaldoAtual(bitcoin, "Bitcoin");
                    ViewBag.QuantidadaEmRealBitcoin = saldoTotalBitcoin.ToString("F2");

                    double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                    ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                    double ethereum = QuantidadeTotalCriptomoeda("Ethereum", user);
                    ViewBag.QuantidadaTotalEthereum = ethereum.ToString("F6");

                    double saldoTotalEthereum = CalcularSaldoAtual(ethereum, "Ethereum");
                    ViewBag.QuantidadaEmRealEthereum = saldoTotalEthereum.ToString("F2");

                    double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                    ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                    double bitcoinCash = QuantidadeTotalCriptomoeda("Bitcoin Cash", user);
                    ViewBag.QuantidadaTotalBitcoinCash = bitcoinCash.ToString("F6");

                    double saldoTotalBitcoinCash = CalcularSaldoAtual(bitcoinCash, "Bitcoin Cash");
                    ViewBag.QuantidadaEmRealBitcoinCash = saldoTotalBitcoinCash.ToString("F2");

                    double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                    ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                    double xrp = QuantidadeTotalCriptomoeda("XRP", user);
                    ViewBag.QuantidadaTotalXrp = xrp.ToString("F6");

                    double saldoTotalXrp = CalcularSaldoAtual(xrp, "XRP");
                    ViewBag.QuantidadaEmRealXrp = saldoTotalXrp.ToString("F2");

                    double valorInvestidoXrp = ValorInvestido("XRP", user);
                    ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                    double paxGold = QuantidadeTotalCriptomoeda("PAX Gold", user);
                    ViewBag.QuantidadaTotalPaxGold = paxGold.ToString("F6");

                    double saldoTotalPaxGold = CalcularSaldoAtual(paxGold, "PAX Gold");
                    ViewBag.QuantidadaEmRealPaxGold = saldoTotalPaxGold.ToString("F2");

                    double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                    ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                    double litecoin = QuantidadeTotalCriptomoeda("Litecoin", user);
                    ViewBag.QuantidadaTotalLitecoin = litecoin.ToString("F6");

                    double saldoTotalLitecoin = CalcularSaldoAtual(litecoin, "Litecoin");
                    ViewBag.QuantidadaEmRealLitecoin = saldoTotalLitecoin.ToString("F2");

                    double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                    ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                    double saldoSemInvestimento = SaldoSemInvestimento(user);
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
                double bitcoin = QuantidadeTotalCriptomoeda("Bitcoin", user);
                ViewBag.QuantidadaTotalBitcoin = bitcoin.ToString("F6");

                double saldoTotalBitcoin = CalcularSaldoAtual(bitcoin, "Bitcoin");
                ViewBag.QuantidadaEmRealBitcoin = saldoTotalBitcoin.ToString("F2");

                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalBitcoin - valorInvestidoBitcoin).ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var Bitcoin = QuantidadeTotalCriptomoedaAdm("Bitcoin");
                ViewBag.Bitcoin = Bitcoin;
                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Bitcoin", Bitcoin);

                var BitcoinValorRSString = ValorTotalCriptomoedaAdm("Bitcoin", Bitcoin).ToString("F2");
                var BitcoinValorRS = Convert.ToDouble(BitcoinValorRSString);
                ViewBag.BitcoinValorRS = BitcoinValorRS;

                var BitcoinInvestidoString = ValorInvestidoAdm("Bitcoin").ToString("F2");
                var BitcoinInvestido = Convert.ToDouble(BitcoinInvestidoString);
                ViewBag.BitcoinInvestido = BitcoinInvestido;

                ViewBag.LucroOuPerdaAdm = (BitcoinValorRS - BitcoinInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("Bitcoin", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorBitcoin7Dias = ValoresDias("Bitcoin", bitcoin, primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorBitcoin7Dias = Valores7Dias("Bitcoin", bitcoin);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorBitcoinMes = ValoresDias("Bitcoin", bitcoin, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorBitcoinMes = Valores30Dias("Bitcoin", bitcoin);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("Bitcoin");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorBitcoinMesAdm = ValoresDias("Bitcoin", Bitcoin, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorBitcoinMesAdm = Valores30Dias("Bitcoin", Bitcoin);
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
                double ethereum = QuantidadeTotalCriptomoeda("Ethereum", user);
                ViewBag.QuantidadaTotalEthereum = ethereum.ToString("F6");

                double saldoTotalEthereum = CalcularSaldoAtual(ethereum, "Ethereum");
                ViewBag.QuantidadaEmRealEthereum = saldoTotalEthereum.ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalEthereum - valorInvestidoEthereum).ToString("F2");



                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var Ethereum = QuantidadeTotalCriptomoedaAdm("Ethereum");
                ViewBag.Ethereum = Ethereum;

                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Ethereum", Ethereum);

                var EthereumValorRString = ValorTotalCriptomoedaAdm("Ethereum", Ethereum).ToString("F2");
                double EthereumValorRS = Convert.ToDouble(EthereumValorRString);
                ViewBag.EthereumValorRS = EthereumValorRS;

                var EthereumInvestidoString = ValorInvestidoAdm("Ethereum").ToString("F2");
                double EthereumInvestido = Convert.ToDouble(EthereumInvestidoString);
                ViewBag.EthereumInvestido = EthereumInvestido;

                ViewBag.LucroOuPerdaAdm = (EthereumValorRS - EthereumInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("Ethereum", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorEthereum7Dias = ValoresDias("Ethereum", ethereum, primeiroInvestimento);
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorEthereum7Dias = Valores7Dias("Ethereum", ethereum);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = ValoresDias("Ethereum", ethereum, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorMes = Valores30Dias("Ethereum", ethereum);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("Ethereum");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = ValoresDias("Ethereum", Ethereum, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorMesAdm = Valores30Dias("Ethereum", Ethereum);
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
                double bitcoinCash = QuantidadeTotalCriptomoeda("Bitcoin Cash", user);
                ViewBag.QuantidadaTotalBitcoinCash = bitcoinCash.ToString("F6");

                double saldoTotalBitcoinCash = CalcularSaldoAtual(bitcoinCash, "Bitcoin Cash");
                ViewBag.QuantidadaEmRealBitcoinCash = saldoTotalBitcoinCash.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalBitcoinCash - valorInvestidoBitcoinCash).ToString("F2");

                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var BitcoinCash = QuantidadeTotalCriptomoedaAdm("Bitcoin Cash");
                ViewBag.BitcoinCash = BitcoinCash;
                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Bitcoin Cash", BitcoinCash);

                var BitcoinCashValorRSString = ValorTotalCriptomoedaAdm("Bitcoin Cash", BitcoinCash).ToString("F2");
                double BitcoinCashValorRS = Convert.ToDouble(BitcoinCashValorRSString);
                ViewBag.BitcoinCashValorRS = BitcoinCashValorRS;

                var BitcoinCashInvestidoString = ValorInvestidoAdm("Bitcoin Cash").ToString("F2");
                double BitcoinCashInvestido = Convert.ToDouble(BitcoinCashInvestidoString);
                ViewBag.BitcoinCashInvestido = BitcoinCashInvestido;

                ViewBag.LucroOuPerdaAdm = (BitcoinCashValorRS - BitcoinCashInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("Bitcoin Cash", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorBitcoinCash7Dias = ValoresDias("Bitcoin Cash", bitcoinCash, primeiroInvestimento);
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorBitcoinCash7Dias = Valores7Dias("Bitcoin Cash", bitcoinCash);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = ValoresDias("Bitcoin Cash", bitcoinCash, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorMes = Valores30Dias("Bitcoin Cash", bitcoinCash);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("Bitcoin Cash");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = ValoresDias("Bitcoin Cash", BitcoinCash, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorMesAdm = Valores30Dias("Bitcoin Cash", BitcoinCash);
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
                double xrp = QuantidadeTotalCriptomoeda("XRP", user);
                ViewBag.QuantidadaTotalXrp = xrp.ToString("F6");

                double saldoTotalXrp = CalcularSaldoAtual(xrp, "XRP");
                ViewBag.QuantidadaEmRealXrp = saldoTotalXrp.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalXrp - valorInvestidoXrp).ToString("F2");

                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var Xrp = QuantidadeTotalCriptomoedaAdm("XRP");
                ViewBag.Xrp = Xrp;
                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("XRP", Xrp);

                var XrpValorRSString = ValorTotalCriptomoedaAdm("XRP", Xrp).ToString("F2");
                var XrpValorRS = Convert.ToDouble(XrpValorRSString);
                ViewBag.XrpValorRS = XrpValorRS;

                var XrpInvestidoString = ValorInvestidoAdm("XRP").ToString("F2");
                var XrpInvestido = Convert.ToDouble(XrpInvestidoString);
                ViewBag.XrpInvestido = XrpInvestido;

                ViewBag.LucroOuPerdaAdm = (XrpValorRS - XrpInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("XRP", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorXrp7Dias = ValoresDias("XRP", xrp, primeiroInvestimento);
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorXrp7Dias = Valores7Dias("XRP", xrp);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = ValoresDias("XRP", xrp, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorMes = Valores30Dias("XRP", xrp);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("XRP");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = ValoresDias("XRP", Xrp, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorMesAdm = Valores30Dias("XRP", Xrp);
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
                double paxGold = QuantidadeTotalCriptomoeda("PAX Gold", user);
                ViewBag.QuantidadaTotalPaxGold = paxGold.ToString("F6");

                double saldoTotalPaxGold = CalcularSaldoAtual(paxGold, "PAX Gold");
                ViewBag.QuantidadaEmRealPaxGold = saldoTotalPaxGold.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalPaxGold - valorInvestidoPaxGold).ToString("F2");

                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                var PaxGold = QuantidadeTotalCriptomoedaAdm("PAX Gold");
                ViewBag.PaxGold = PaxGold;
                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("PAX Gold", PaxGold);

                var PaxGoldValorRSString = ValorTotalCriptomoedaAdm("PAX Gold", PaxGold).ToString("F2");
                double PaxGoldValorRS = Convert.ToDouble(PaxGoldValorRSString);
                ViewBag.PaxGoldValorRS = PaxGoldValorRS;

                var PaxGoldInvestidoString = ValorInvestidoAdm("PAX Gold").ToString("F2");
                double PaxGoldInvestido = Convert.ToDouble(PaxGoldInvestidoString);
                ViewBag.PaxGoldInvestido = PaxGoldInvestido;

                ViewBag.LucroOuPerdaAdm = (PaxGoldValorRS - PaxGoldInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("PAX Gold", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorPaxGold7Dias = ValoresDias("PAX Gold", paxGold, primeiroInvestimento);
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorPaxGold7Dias = Valores7Dias("PAX Gold", paxGold);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = ValoresDias("PAX Gold", paxGold, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorMes = Valores30Dias("PAX Gold", paxGold);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("PAX Gold");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = ValoresDias("PAX Gold", PaxGold, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorMesAdm = Valores30Dias("PAX Gold", PaxGold);
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
                double litecoin = QuantidadeTotalCriptomoeda("Litecoin", user);
                ViewBag.QuantidadaTotalLitecoin = litecoin.ToString("F6");

                double saldoTotalLitecoin = CalcularSaldoAtual(litecoin, "Litecoin");
                ViewBag.QuantidadaEmRealLitecoin = saldoTotalLitecoin.ToString("F2");

                double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
                ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

                ViewBag.LucroOuPerda = (saldoTotalLitecoin - valorInvestidoLitecoin).ToString("F2");

                double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
                ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");

                double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
                ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

                double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
                ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

                double valorInvestidoXrp = ValorInvestido("XRP", user);
                ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

                double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
                ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

                var Litecoin = QuantidadeTotalCriptomoedaAdm("Litecoin");
                ViewBag.Litecoin = Litecoin;
                ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Litecoin", Litecoin);

                var LitecoinValorRSString = ValorTotalCriptomoedaAdm("Litecoin", Litecoin).ToString("F2");
                var LitecoinValorRS = Convert.ToDouble(LitecoinValorRSString);
                ViewBag.LitecoinValorRS = LitecoinValorRS;

                var LitecoinInvestidoString = ValorInvestidoAdm("Litecoin").ToString("F2");
                var LitecoinInvestido = Convert.ToDouble(LitecoinInvestidoString);
                ViewBag.LitecoinInvestido = LitecoinInvestido;

                ViewBag.LucroOuPerdaAdm = (LitecoinValorRS - LitecoinInvestido).ToString("F2");

                int primeiroInvestimento = DataPrimeiroInvestimento("Litecoin", user);

                if (primeiroInvestimento < 7)
                {
                    ViewBag.ValorLitecoin7Dias = ValoresDias("Litecoin", litecoin, primeiroInvestimento);
                    ViewBag.Ultimos7Dias = UltimosDias(primeiroInvestimento);
                }
                else
                {
                    ViewBag.ValorLitecoin7Dias = Valores7Dias("Litecoin", litecoin);
                    ViewBag.Ultimos7Dias = Ultimos7Dias();
                }

                if (primeiroInvestimento <= 30)
                {
                    ViewBag.UltimoMes = UltimosDias(primeiroInvestimento);
                    ViewBag.ValorMes = ValoresDias("Litecoin", litecoin, primeiroInvestimento);
                }
                else
                {
                    ViewBag.UltimoMes = Ultimos30Dias();
                    ViewBag.ValorMes = Valores30Dias("Litecoin", litecoin);
                }

                int primeiroInvestimentoGeralAdm = DataPrimeiroInvestimentoAdm("Litecoin");
                if (primeiroInvestimentoGeralAdm <= 30)
                {
                    ViewBag.UltimoMesAdm = UltimosDias(primeiroInvestimentoGeralAdm);
                    ViewBag.ValorMesAdm = ValoresDias("Litecoin", Litecoin, primeiroInvestimentoGeralAdm);
                }
                else
                {
                    ViewBag.UltimoMesAdm = Ultimos30Dias();
                    ViewBag.ValorMesAdm = Valores30Dias("Litecoin", Litecoin);
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
        }

        public double ValorInvestido(string criptomoeda, string user)
        {
            var investimentos = (from transacoes in _context.Transacao
                                 join conta in _context.ContaCliente
                                 on transacoes.ContaClienteId equals conta.Id
                                 join usuario in _context.ApplicationUser
                                 on conta.ApplicationUserID equals usuario.Id
                                 join criptomoedahoje in _context.CriptomoedaHoje
                                 on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
                                 join criptomoedas in _context.Criptomoeda
                                 on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                                 join criptosaldo in _context.CriptoSaldo
                                 on transacoes.CriptoSaldoId equals criptosaldo.Id
                                 where criptomoedas.Nome == criptomoeda &&
                                 usuario.UserName == user &&
                                 transacoes.Tipo.Equals(TipoTransacao.Compra)
                                 select transacoes.Valor).Sum();

            var investimentosVenda = (from transacoes in _context.Transacao
                                      join conta in _context.ContaCliente
                                      on transacoes.ContaClienteId equals conta.Id
                                      join usuario in _context.ApplicationUser
                                      on conta.ApplicationUserID equals usuario.Id
                                      join criptomoedahoje in _context.CriptomoedaHoje
                                      on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
                                      join criptomoedas in _context.Criptomoeda
                                      on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                                      join criptosaldo in _context.CriptoSaldo
                                      on transacoes.CriptoSaldoId equals criptosaldo.Id
                                      where criptomoedas.Nome == criptomoeda &&
                                      usuario.UserName == user &&
                                      transacoes.Tipo.Equals(TipoTransacao.Venda)
                                      select transacoes.Valor).Sum();

            return investimentos - investimentosVenda;
        }

        public double ValorInvestidoAdm(string criptomoeda)
        {
            var investimentos = (from transacoes in _context.Transacao
                                 join conta in _context.ContaCliente
                                 on transacoes.ContaClienteId equals conta.Id
                                 join criptomoedahoje in _context.CriptomoedaHoje
                                 on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
                                 join criptomoedas in _context.Criptomoeda
                                 on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                                 join criptosaldo in _context.CriptoSaldo
                                 on transacoes.CriptoSaldoId equals criptosaldo.Id
                                 where criptomoedas.Nome == criptomoeda &&
                                 transacoes.Tipo.Equals(TipoTransacao.Compra)
                                 select transacoes.Valor).Sum();

            var investimentosVenda = (from transacoes in _context.Transacao
                                      join conta in _context.ContaCliente
                                      on transacoes.ContaClienteId equals conta.Id
                                      join criptomoedahoje in _context.CriptomoedaHoje
                                      on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
                                      join criptomoedas in _context.Criptomoeda
                                      on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                                      join criptosaldo in _context.CriptoSaldo
                                      on transacoes.CriptoSaldoId equals criptosaldo.Id
                                      where criptomoedas.Nome == criptomoeda &&
                                      transacoes.Tipo.Equals(TipoTransacao.Venda)
                                      select transacoes.Valor).Sum();

            return investimentos - investimentosVenda;
        }

        public List<int> Ultimos7Dias()
        {
            var diasList = new List<int>();

            DateTime diasSete = DateTime.Today;

            for (int i = 6; i >= 0; i--)
            {
                diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                diasList.Add(diasSete.Day);
            }

            return diasList;
        }

        public List<double> Valores7Dias(string nome, double quantidadeTotalCriptomoeda)
        {
            var valorList = new List<double>();

            for (int i = 6; i >= 0; i--)
            {

                DateTime diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                DateTime data = diasSete;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();

                var totalDia = (quantidadeTotalCriptomoeda * valor).ToString("F2");

                valorList.Add(Convert.ToDouble(totalDia));
            }

            return valorList;
        }

        public List<double> Valores7DiasAdm(string nome, double quantidadeTotalCriptomoeda)
        {

            var valorList = new List<double>();

            for (int i = 6; i >= 0; i--)
            {

                DateTime diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-i);
                DateTime data = diasSete;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Sum();

                var totalDia = (quantidadeTotalCriptomoeda * valor).ToString("F2");

                valorList.Add(Convert.ToDouble(totalDia));
            }
            return valorList;
        }

        public double QuantidadeTotalCriptomoedaAdm(string criptomoeda)
        {
            var cripto = (from transacoes in _context.Transacao
                          join conta in _context.ContaCliente
                          on transacoes.ContaClienteId equals conta.Id
                          join criptomoedahoje in _context.CriptomoedaHoje
                          on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
                          join criptomoedas in _context.Criptomoeda
                          on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                          join criptosaldo in _context.CriptoSaldo
                          on transacoes.CriptoSaldoId equals criptosaldo.Id
                          where criptomoedas.Nome == criptomoeda
                          select criptosaldo.Quantidade).Sum();
            return cripto;
        }

        public double ValorTotalCriptomoedaAdm(string criptomoeda, double quantidadeCripto)
        {
            var cripto = (from criptomoedahoje in _context.CriptomoedaHoje
                          join criptomoedas in _context.Criptomoeda
                          on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                          where criptomoedas.Nome == criptomoeda
                          orderby criptomoedahoje.Id descending
                          select criptomoedahoje.Valor).FirstOrDefault();

            return cripto * quantidadeCripto;
        }

        public int DataPrimeiroInvestimento(string criptomoeda, string user)
        {
            var primeiraData = (from transacao in _context.Transacao
                                join contacliente in _context.ContaCliente
                                on transacao.ContaClienteId equals contacliente.Id
                                join usuario in _context.ApplicationUser
                                on contacliente.ApplicationUserID equals usuario.Id
                                where usuario.UserName == user
                                join criptohoje in _context.CriptomoedaHoje
                                on transacao.CriptomoedaHojeId equals criptohoje.Id
                                join cripto in _context.Criptomoeda
                                on criptohoje.CriptomoedaId equals cripto.Id
                                where cripto.Nome == criptomoeda
                                orderby transacao.Data ascending
                                select transacao.Data).FirstOrDefault();

            DateTime hoje = DateTime.Today;

            return hoje.Subtract(primeiraData).Days;
        }

        public int DataPrimeiroInvestimentoAdm(string criptomoeda)
        {
            var primeiraData = (from transacao in _context.Transacao
                                join contacliente in _context.ContaCliente
                                on transacao.ContaClienteId equals contacliente.Id
                                join usuario in _context.ApplicationUser
                                on contacliente.ApplicationUserID equals usuario.Id
                                join criptohoje in _context.CriptomoedaHoje
                                on transacao.CriptomoedaHojeId equals criptohoje.Id
                                join cripto in _context.Criptomoeda
                                on criptohoje.CriptomoedaId equals cripto.Id
                                where cripto.Nome == criptomoeda
                                orderby transacao.Data ascending
                                select transacao.Data).FirstOrDefault();

            DateTime hoje = DateTime.Today;

            return hoje.Subtract(primeiraData).Days;
        }

        public List<int> UltimosDias(int dias) 
        {
            var diasList = new List<int>();
            DateTime diaAtual;

            for (int i = dias; i >= 0; i--)
            {
                diaAtual = DateTime.Today;
                diaAtual = diaAtual.AddDays(-i);
                diasList.Add(diaAtual.Day);
            }

            return diasList;
        }

        public List<double> ValoresDias(string nome, double quantidadeTotalCriptomoeda, int dias) 
        {
            var valorList = new List<double>();

            for (int i = dias; i >= 0; i--)
            {
                DateTime diasTotal = DateTime.Today;
                diasTotal = diasTotal.AddDays(-i);
                DateTime data = diasTotal;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();

                var totalDia = (quantidadeTotalCriptomoeda * valor).ToString("F2");

                valorList.Add(Convert.ToDouble(totalDia));
            }

            return valorList;
        }

        public List<int> Ultimos30Dias()
        {
            var diasList = new List<int>();
            DateTime diaAtual;

            for (int i = 31; i >= 0; i--)
            {
                diaAtual = DateTime.Today;
                diaAtual = diaAtual.AddDays(-i);
                diasList.Add(diaAtual.Day);
            }

            return diasList;
        }

        public List<double> Valores30Dias(string nome, double quantidadeTotalCriptomoeda)
        {
            var valorList = new List<double>();

            for (int i = 31; i >= 0; i--)
            {

                DateTime diasTotal = DateTime.Today;
                diasTotal = diasTotal.AddDays(-i);
                DateTime data = diasTotal;

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();

                var totalDia = (quantidadeTotalCriptomoeda * valor).ToString("F2");

                valorList.Add(Convert.ToDouble(totalDia));
            }

            return valorList;
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
