using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;

namespace BlockTechMVC.Controllers
{
    public class CoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoreController(ApplicationDbContext context) 
        {
            _context = context;
        }

        protected double SaldoSemInvestimento(string user)
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

        protected double QuantidadeTotalCriptomoeda(string criptomoeda, string user)
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
                                 usuario.UserName == user
                                 select criptosaldo.Quantidade).FirstOrDefault();

            return investimentos;
        }

        protected double CalcularSaldoAtual(double quantidadeCriptomoeda, string criptomoeda)
        {
            var valorAtual = (from criptohoje in _context.CriptomoedaHoje
                              join cripto in _context.Criptomoeda
                              on criptohoje.CriptomoedaId equals cripto.Id
                              where cripto.Nome == criptomoeda &&
                              criptohoje.Data == DateTime.Today
                              select criptohoje.Valor).Single();

            return quantidadeCriptomoeda * valorAtual;
        }


    }
}
