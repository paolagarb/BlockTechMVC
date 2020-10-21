using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Saldo
    {
        public int Id { get; set; }

        public List<Transacao> Transacao { get; set; } = new List<Transacao>();
        public int TransacaoId { get; set; }

        public CriptoSaldo CriptoSaldo { get; set; } = new CriptoSaldo();
        public int CriptoSaldoId { get; set; }

        [Display(Name = "Saldo Atual")]
        [Column(TypeName = "decimal(20,2)")]
        public double SaldoAtualRS { get; set; } = 0;

        public Saldo()
        {

        }

        public void AddTransacao(Transacao transacao)
        {
            CriptoSaldo.Criptomoeda = transacao.CriptomoedaHoje.Criptomoeda.Nome;

            Transacao.Add(transacao);
            if (transacao.Tipo == Enums.TipoTransacao.Compra)
            {
                SaldoAtualRS -= transacao.Valor;
                CriptoSaldo.Quantidade += transacao.CalcularQuantidadeCripto();
            }
            if (transacao.Tipo == Enums.TipoTransacao.Venda)
            {
                SaldoAtualRS += transacao.Valor;
                CriptoSaldo.Quantidade -= transacao.CalcularQuantidadeCripto();
            }
            if (transacao.Tipo == Enums.TipoTransacao.Transferencia)
            {
                SaldoAtualRS -= transacao.Valor;
            }
        }
    }
}
