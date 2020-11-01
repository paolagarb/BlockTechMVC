using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;
using BlockTechMVC.Models.Enums;

namespace BlockTechMVC.Controllers
{
    public class MeusInvestimentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeusInvestimentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeusInvestimentos
        public IActionResult Index()
        {
            var user = User.Identity.Name;

            if (user == "Administrador")
            {
                var Bitcoin = QuantidadeTotalCriptomoedaAdm("Bitcoin");
                ViewBag.Bitcoin = Bitcoin;
                ViewBag.BitcoinValorRS = ValorTotalCriptomoedaAdm("Bitcoin", Bitcoin).ToString("F2");
                
                var Ethereum = QuantidadeTotalCriptomoedaAdm("Ethereum");
                ViewBag.Ethereum = Ethereum;
                ViewBag.EthereumValorRS = ValorTotalCriptomoedaAdm("Ethereum", Ethereum).ToString("F2");

                var BitcoinCash = QuantidadeTotalCriptomoedaAdm("Bitcoin Cash");
                ViewBag.BitcoinCash = BitcoinCash;
                ViewBag.BitcoinCashValorRS = ValorTotalCriptomoedaAdm("Bitcoin Cash", BitcoinCash).ToString("F2");

                var Litecoin = QuantidadeTotalCriptomoedaAdm("Litecoin");
                ViewBag.Litecoin = Litecoin;
                ViewBag.LitecoinValorRS = ValorTotalCriptomoedaAdm("Litecoin", Litecoin).ToString("F2");

                var PaxGold = QuantidadeTotalCriptomoedaAdm("PAX Gold");
                ViewBag.PaxGold = PaxGold;
                ViewBag.PaxGoldValorRS = ValorTotalCriptomoedaAdm("PAX Gold", PaxGold).ToString("F2");

                var Xrp = QuantidadeTotalCriptomoedaAdm("XRP");
                ViewBag.Xrp = Xrp;
                ViewBag.XrpValorRS = ValorTotalCriptomoedaAdm("XRP", Xrp).ToString("F2");

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
        public double QuantidadeTotalCriptomoeda(string criptomoeda, string user)
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
                                 select criptosaldo.Quantidade).FirstOrDefault();
                                 //Single();
            return investimentos;
        }

        public double CalcularSaldoAtual(double quantidadeCriptomoeda, string criptomoeda)
        {
            var valorAtual = (from criptohoje in _context.CriptomoedaHoje
                              join cripto in _context.Criptomoeda
                              on criptohoje.CriptomoedaId equals cripto.Id
                              where cripto.Nome == criptomoeda &&
                              criptohoje.Data == DateTime.Today
                              select criptohoje.Valor).Single();
                              //FirstOrDefault();

            return quantidadeCriptomoeda * valorAtual;
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
                                 select transacoes.Valor).FirstOrDefault();
            //Single();
            return investimentos;
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
            //Single();
            return investimentos;
        }

        public double SaldoSemInvestimento(string user)
        {
            var saldoNaoInvestido = (from transacoes in _context.Transacao
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
                                 join saldo in _context.Saldo
                                 on transacoes.SaldoId equals saldo.Id
                                 where usuario.UserName == user 
                                 select saldo.SaldoAtualRS).FirstOrDefault();
            return saldoNaoInvestido;
        }

        public ActionResult Bitcoin()
        {
            var user = User.Identity.Name;

            double bitcoin = QuantidadeTotalCriptomoeda("Bitcoin", user);
            ViewBag.QuantidadaTotalBitcoin = bitcoin.ToString("F6");

            double saldoTotalBitcoin = CalcularSaldoAtual(bitcoin, "Bitcoin");
            ViewBag.QuantidadaEmRealBitcoin = saldoTotalBitcoin.ToString("F2");

            double valorInvestidoBitcoin = ValorInvestido("Bitcoin", user);
            ViewBag.ValorInvestidoBitcoin = valorInvestidoBitcoin.ToString("F2");


            ViewBag.LucroOuPerda = (saldoTotalBitcoin - valorInvestidoBitcoin).ToString("F2");
           
            ViewBag.ValorBitcoin7Dias = Valores7Dias("Bitcoin", bitcoin);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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
            ViewBag.BitcoinValorRS = ValorTotalCriptomoedaAdm("Bitcoin", Bitcoin).ToString("F2");
            ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Bitcoin", Bitcoin);
            ViewBag.BitcoinInvestido = ValorInvestidoAdm("Bitcoin");

            return View();
        }

        public ActionResult Ethereum()
        {
            var user = User.Identity.Name;

            double ethereum = QuantidadeTotalCriptomoeda("Ethereum", user);
            ViewBag.QuantidadaTotalEthereum = ethereum.ToString("F6");

            double saldoTotalEthereum = CalcularSaldoAtual(ethereum, "Ethereum");
            ViewBag.QuantidadaEmRealEthereum = saldoTotalEthereum.ToString("F2");

            double valorInvestidoEthereum = ValorInvestido("Ethereum", user);
            ViewBag.ValorInvestidoEthereum = valorInvestidoEthereum.ToString("F2");

            ViewBag.LucroOuPerda = (saldoTotalEthereum - valorInvestidoEthereum).ToString("F2");
           
            ViewBag.ValorEthereum7Dias = Valores7Dias("Ethereum", ethereum);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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
            ViewBag.EthereumValorRS = ValorTotalCriptomoedaAdm("Ethereum", Ethereum).ToString("F2");
            ViewBag.Ultimos7DiasAdm = Valores7DiasAdm("Ethereum", Ethereum);
            ViewBag.EthereumInvestido = ValorInvestidoAdm("Ethereum");

            return View();
        }
        
        public ActionResult BitcoinCash()
        {
            var user = User.Identity.Name;

            double bitcoinCash = QuantidadeTotalCriptomoeda("Bitcoin Cash", user);
            ViewBag.QuantidadaTotalBitcoinCash = bitcoinCash.ToString("F6");

            double saldoTotalBitcoinCash = CalcularSaldoAtual(bitcoinCash, "Bitcoin Cash");
            ViewBag.QuantidadaEmRealBitcoinCash = saldoTotalBitcoinCash.ToString("F2");

            double valorInvestidoBitcoinCash = ValorInvestido("Bitcoin Cash", user);
            ViewBag.ValorInvestidoBitcoinCash = valorInvestidoBitcoinCash.ToString("F2");

            ViewBag.LucroOuPerda = (saldoTotalBitcoinCash - valorInvestidoBitcoinCash).ToString("F2");
           
            ViewBag.ValorBitcoinCash7Dias = Valores7Dias("Bitcoin Cash", bitcoinCash);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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

            return View();
        }
        
        public ActionResult Xrp()
        {
            var user = User.Identity.Name;

            double xrp = QuantidadeTotalCriptomoeda("XRP", user);
            ViewBag.QuantidadaTotalXrp = xrp.ToString("F6");

            double saldoTotalXrp = CalcularSaldoAtual(xrp, "XRP");
            ViewBag.QuantidadaEmRealXrp = saldoTotalXrp.ToString("F2");

            double valorInvestidoXrp = ValorInvestido("XRP", user);
            ViewBag.ValorInvestidoXrp = valorInvestidoXrp.ToString("F2");

            ViewBag.LucroOuPerda = (saldoTotalXrp - valorInvestidoXrp).ToString("F2");
           
            ViewBag.ValorXrp7Dias = Valores7Dias("XRP", xrp);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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

            return View();
        }

        public ActionResult PaxGold()
        {
            var user = User.Identity.Name;

            double paxGold = QuantidadeTotalCriptomoeda("PAX Gold", user);
            ViewBag.QuantidadaTotalPaxGold = paxGold.ToString("F6");

            double saldoTotalPaxGold = CalcularSaldoAtual(paxGold, "PAX Gold");
            ViewBag.QuantidadaEmRealPaxGold = saldoTotalPaxGold.ToString("F2");

            double valorInvestidoPaxGold = ValorInvestido("PAX Gold", user);
            ViewBag.ValorInvestidoPaxGold = valorInvestidoPaxGold.ToString("F2");

            ViewBag.LucroOuPerda = (saldoTotalPaxGold - valorInvestidoPaxGold).ToString("F2");
           
            ViewBag.ValorPaxGold7Dias = Valores7Dias("PAX Gold", paxGold);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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

            return View();
        }
        
        public ActionResult Litecoin()
        {
            var user = User.Identity.Name;

            double litecoin = QuantidadeTotalCriptomoeda("Litecoin", user);
            ViewBag.QuantidadaTotalLitecoin = litecoin.ToString("F6");

            double saldoTotalLitecoin = CalcularSaldoAtual(litecoin, "Litecoin");
            ViewBag.QuantidadaEmRealLitecoin = saldoTotalLitecoin.ToString("F2");

            double valorInvestidoLitecoin = ValorInvestido("Litecoin", user);
            ViewBag.ValorInvestidoLitecoin = valorInvestidoLitecoin.ToString("F2");

            ViewBag.LucroOuPerda = (saldoTotalLitecoin - valorInvestidoLitecoin).ToString("F2");
           
            ViewBag.ValorLitecoin7Dias = Valores7Dias("Litecoin", litecoin);
            ViewBag.Ultimos7Dias = Ultimos7Dias();

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


            return View();
        }

        public List<int> Ultimos7Dias()
        {
            var diasList = new List<int>();

            for (int i = 1; i <= 7; i++)
            {
                diasList.Add((DateTime.Now.Day - 7) + i);
            }

            return diasList;
        }

        public List<double> Valores7Dias(string nome, double quantidadeTotalCriptomoeda)
        {

            var valorList = new List<double>();

            for (int i = 1; i <= 7; i++)
            {
                var data = (DateTime.Now.Day - 7) + i;
                DateTime dia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, data);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Equals(dia)
                             select criptohoje.Valor).Single();

                var totalDia = (quantidadeTotalCriptomoeda * valor).ToString("F2");

                valorList.Add(Convert.ToDouble(totalDia));
            }

            return valorList;
        }
        public List<double> Valores7DiasAdm(string nome, double quantidadeTotalCriptomoeda)
        {

            var valorList = new List<double>();

            for (int i = 1; i <= 7; i++)
            {
                var data = (DateTime.Now.Day - 7) + i;
                DateTime dia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, data);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Equals(dia)
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
                                 where criptomoedas.Nome == criptomoeda &&
                                 transacoes.Tipo.Equals(TipoTransacao.Compra)
                                 select criptosaldo.Quantidade).Sum();
            return cripto;
        }

        public double ValorTotalCriptomoedaAdm(string criptomoeda, double quantidadeCripto)
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
                                 where criptomoedas.Nome == criptomoeda &&
                                 transacoes.Tipo.Equals(TipoTransacao.Compra)
                                 select criptomoedahoje.Valor).FirstOrDefault();

            return cripto * quantidadeCripto;
        }

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }
    }
}
