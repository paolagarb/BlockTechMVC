using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Saldo
    {
        public int Id { get; set; }
        public List<Transacao> Transacao { get; set; } = new List<Transacao>();
        public int TransacaoId { get; set; }
        public double SaldoAtualRS { get; set; } = 0;
        public double quantidadeCripo { get; set; } = 0;

        public Saldo()
        {

        }
        public Saldo( double saldoAtualRS, double quantidadeCripo, Transacao transacao)
        {
            SaldoAtualRS = saldoAtualRS;
            this.quantidadeCripo = quantidadeCripo;
            AddTransacao(transacao);
        }

        public void AddTransacao(Transacao transacao)
        {
            Transacao.Add(transacao);
            if (transacao.Tipo == Enums.TipoTransacao.Compra)
            {
                SaldoAtualRS -= transacao.Valor;
                quantidadeCripo += transacao.CalcularQuantidadeCripto();
            }
            if (transacao.Tipo == Enums.TipoTransacao.Venda)
            {
                SaldoAtualRS += transacao.Valor;
                quantidadeCripo -= transacao.CalcularQuantidadeCripto();
            }
            if (transacao.Tipo == Enums.TipoTransacao.Transferencia)
            {
                SaldoAtualRS += transacao.Valor;
            }
        }
    }
}
