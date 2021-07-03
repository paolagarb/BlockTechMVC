using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockTechMVC.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly ApplicationDbContext _context;

        public RelatorioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<double> PorcentualUltimosDias(int quantidadeDias, string cripto) 
        {
            var porcentagem = new List<double>();
            var date = DateTime.Today;
            date = date.AddDays(quantidadeDias + 1);

            var valor = (from coin in _context.Criptomoeda
                         join criptohoje in _context.CriptomoedaHoje
                         on coin.Id equals criptohoje.CriptomoedaId
                         where coin.Nome == cripto && criptohoje.Data.Date.Equals(date.Date)
                         select criptohoje.Valor).Single();

            for (int i = quantidadeDias; i >= 0; i--)
            {
                DateTime dia = DateTime.Today;
                dia = dia.AddDays(-i);

                var valorDia = (from coin in _context.Criptomoeda
                                join criptohoje in _context.CriptomoedaHoje
                                on coin.Id equals criptohoje.CriptomoedaId
                                where coin.Nome == cripto && criptohoje.Data.Date.Equals(dia.Date)
                                select criptohoje.Valor).Single();

                var regra = ((valorDia * 100.0) / valor);
                var resultado = regra - 100;
                valor = valorDia;

                var duasCasas = resultado.ToString("F2");
                var duasCasasDouble = Convert.ToDouble(duasCasas);

                porcentagem.Add(duasCasasDouble);
            }

            return porcentagem;
        }

        public List<double> SaldoUltimosDias(int quantidadeDias, string cripto) 
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

                list.Add(valor);
            }

            return list;
        }

        public double ValorAtualCriptomoeda(string cripto)
        {
            return (from coin in _context.Criptomoeda
                    join criptohoje in _context.CriptomoedaHoje
                    on coin.Id equals criptohoje.CriptomoedaId
                    where coin.Nome == cripto && criptohoje.Data == DateTime.Today
                    select criptohoje.Valor).Single();
        }
    }
}
