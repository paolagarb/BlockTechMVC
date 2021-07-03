using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Repositories
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private readonly ApplicationDbContext _context;

        public InvestimentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int PrimeiroInvestimento(string cripto, string user)
        {
            var primeiraData = (from transacao in _context.Transacao
                                join contacliente in _context.ContaCliente
                                on transacao.ContaClienteId equals contacliente.Id
                                join usuario in _context.ApplicationUser
                                on contacliente.ApplicationUserID equals usuario.Id
                                where usuario.UserName == user
                                join criptohoje in _context.CriptomoedaHoje
                                on transacao.CriptomoedaHojeId equals criptohoje.Id
                                join criptomoeda in _context.Criptomoeda
                                on criptohoje.CriptomoedaId equals criptomoeda.Id
                                where criptomoeda.Nome == cripto
                                orderby transacao.Data ascending
                                select transacao.Data).FirstOrDefault();

            DateTime data = DateTime.Today;
            return data.Subtract(primeiraData).Days;
        }

        public int PrimeiroInvestimentoAd(string cripto)
        {
            throw new NotImplementedException();
        }

        public int QuantidadeCriptomoedaAdm(string cripto)
        {
            var primeiraData = (from transacao in _context.Transacao
                                join contacliente in _context.ContaCliente
                                on transacao.ContaClienteId equals contacliente.Id
                                join usuario in _context.ApplicationUser
                                on contacliente.ApplicationUserID equals usuario.Id
                                join criptohoje in _context.CriptomoedaHoje
                                on transacao.CriptomoedaHojeId equals criptohoje.Id
                                join criptomoeda in _context.Criptomoeda
                                on criptohoje.CriptomoedaId equals criptomoeda.Id
                                where criptomoeda.Nome == cripto
                                orderby transacao.Data ascending
                                select transacao.Data).FirstOrDefault();

            DateTime data = DateTime.Today;
            return data.Subtract(primeiraData).Days;
        }

        public double ValorCriptomoedaAdm(string cripto, double quantidade)
        {
            var valor = (from criptomoedahoje in _context.CriptomoedaHoje
                          join criptomoedas in _context.Criptomoeda
                          on criptomoedahoje.CriptomoedaId equals criptomoedas.Id
                          where criptomoedas.Nome == cripto
                          orderby criptomoedahoje.Id descending
                          select criptomoedahoje.Valor).FirstOrDefault();

            return valor * quantidade;
        }

        public double ValorInvestido(string cripto, string user)
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
                                where criptomoedas.Nome == cripto &&
                                usuario.UserName == user &&
                                transacoes.Tipo.Equals(TipoTransacao.Compra)
                                select transacoes.Valor).Sum();

            var investimentoVenda = (from transacoes in _context.Transacao
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
                                     where criptomoedas.Nome == cripto &&
                                     usuario.UserName == user &&
                                     transacoes.Tipo.Equals(TipoTransacao.Venda)
                                     select transacoes.Valor).Sum();

            return investimento - investimentoVenda;
        }

        public double ValorInvestidoAdm(string cripto)
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
                                 where criptomoedas.Nome == cripto &&
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
                                      where criptomoedas.Nome == cripto &&
                                      transacoes.Tipo.Equals(TipoTransacao.Venda)
                                      select transacoes.Valor).Sum();

            return investimentos - investimentosVenda;
        }

        public List<double> ValorTotalUltimosDias(int quantidadeDias, double totalCripto, string cripto)
        {
            var list = new List<double>();

            for (int i = quantidadeDias; i >= 0; i--)
            {
                DateTime data = DateTime.Today;
                data = data.AddDays(-i);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == cripto && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();

                var totalDia = (totalCripto * valor).ToString("F2");

                list.Add(Convert.ToDouble(totalDia));
            }

            return list;
        }

        public List<double> ValorTotalUltimosDiasAdm(int quantidadeDias, string cripto, double totalCripto)
        {
            var list = new List<double>();

            for (int i = quantidadeDias; i > 0; i--)
            {
                DateTime data = DateTime.Today;
                data = data.AddDays(-i);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == cripto && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Sum();

                var totalDia = (totalCripto * valor).ToString("F2");
                list.Add(Convert.ToDouble(totalDia));
            }
            return list;
        }
    }
}
