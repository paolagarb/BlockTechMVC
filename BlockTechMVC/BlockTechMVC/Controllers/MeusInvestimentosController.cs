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

                double saldoSemInvestimento = SaldoSemInvestimento(user);
                ViewBag.SaldoSemInvestimento = saldoSemInvestimento.ToString("F2");

                double dinheiroTotalConta = saldoSemInvestimento + saldoTotalBitcoin; //adicionar todas criptomoedas
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
                              select criptohoje.Valor).FirstOrDefault();
            //Single();
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

        //public double CriptomoedaTotalCompra(string criptomoeda, string user)
        //{
        //    var investimentos = (from transacoes in _context.Transacao
        //                         join conta in _context.ContaCliente
        //                         on transacoes.ContaClienteId equals conta.Id
        //                         join usuario in _context.ApplicationUser
        //                         on conta.ApplicationUserID equals usuario.Id
        //                         join criptomoedahoje in _context.CriptomoedaHoje
        //                         on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
        //                         join criptomoedas in _context.Criptomoeda
        //                         on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
        //                         join criptosaldo in _context.CriptoSaldo
        //                         on transacoes.CriptoSaldoId equals criptosaldo.Id
        //                         where criptomoedas.Nome == criptomoeda &&
        //                         usuario.UserName == user &&
        //                         transacoes.Tipo.Equals(TipoTransacao.Compra)
        //                         select transacoes.Valor).Single();
        //    return investimentos;
        //}

        //public object CriptomoedaTotalVenda(string criptomoeda, string user)
        //{
        //    var investimentos = (from transacoes in _context.Transacao
        //                         join conta in _context.ContaCliente
        //                         on transacoes.ContaClienteId equals conta.Id
        //                         join usuario in _context.ApplicationUser
        //                         on conta.ApplicationUserID equals usuario.Id
        //                         join criptomoedahoje in _context.CriptomoedaHoje
        //                         on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
        //                         join criptomoedas in _context.Criptomoeda
        //                         on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
        //                         where criptomoedas.Nome == criptomoeda &&
        //                         usuario.UserName == user &&
        //                         transacoes.Tipo.Equals(TipoTransacao.Venda)
        //                         select transacoes.Valor).Sum();
        //    return investimentos;
        //}

        //public double CriptomoedaTotalTransferencia(string criptomoeda, string user)
        //{
        //    var investimentos = (from transacoes in _context.Transacao
        //                         join conta in _context.ContaCliente
        //                         on transacoes.ContaClienteId equals conta.Id
        //                         join usuario in _context.ApplicationUser
        //                         on conta.ApplicationUserID equals usuario.Id
        //                         join criptomoedahoje in _context.CriptomoedaHoje
        //                         on transacoes.CriptomoedaHojeId equals criptomoedahoje.Id
        //                         join criptomoedas in _context.Criptomoeda
        //                         on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
        //                         where criptomoedas.Nome == criptomoeda &&
        //                         transacoes.Tipo.Equals(TipoTransacao.Transferencia) &&
        //                         usuario.UserName == user &&
        //                         transacoes.Tipo.Equals(TipoTransacao.Transferencia)
        //                         select transacoes.Valor).Sum();
        //    return investimentos;
        //}

        private bool TransacaoExists(int id)
        {
            return _context.Transacao.Any(e => e.Id == id);
        }
    }
}
