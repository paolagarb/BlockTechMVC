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

        [Display(Name ="Saldo Atual")]
        [Column(TypeName = "decimal(20,2)")]
        public double SaldoAtualRS { get; set; } = 0;

        [Display(Name ="Saldo em Criptomoeda")]


        [Column(TypeName = "decimal(20,2)")]
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
