using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockTechMVC.Repositories
{
    public class CalculoRepository : ICalculoRepository
    {
        private readonly ApplicationDbContext _context;

        public CalculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public double TotalCriptomoeda(string criptomoeda, string user)
        {
            var investimento = (from transacoes in _context.Transacao
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

            return investimento;
        }

        public double TotalNaoInvestido(string user)
        {
            var naoInvestido = (from transacoes in _context.Transacao
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
            return naoInvestido;
        }

        public double SaldoAtual(double quantidadeCriptomoeda, string criptomoeda)
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
