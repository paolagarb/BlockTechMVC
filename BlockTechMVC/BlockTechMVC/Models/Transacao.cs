using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [Display(Name ="Transação")]
        public TipoTransacao Tipo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        //[Column(TypeName = "decimal(20,2)")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }

        [Display(Name = "Criptomoeda")]
        public int CriptomoedaHojeId { get; set; }
        public CriptomoedaHoje CriptomoedaHoje { get; set; }

        [Display(Name = "Conta")]
        public int ContaClienteId { get; set; }
        public ContaCliente ContaCliente { get; set; }

        [Display(Name = "Saldo")]
        public CriptoSaldo CriptoSaldo { get; set; }
        public int CriptoSaldoId { get; set; }

        [Display(Name = "Saldo")]
        public int SaldoId { get; set; }
        public Saldo Saldo { get; set; }

        public Transacao()
        {

        }

        public Transacao(TipoTransacao tipo, DateTime data, double valor, CriptomoedaHoje criptomoedaHoje, ContaCliente contaCliente)
        {
            Tipo = tipo;
            Data = data;
            Valor = valor;
            CriptomoedaHoje = criptomoedaHoje;
            ContaCliente = contaCliente;

            CriptoSaldo.Criptomoeda = CriptomoedaHoje.Criptomoeda.Nome;
            Quantidade();
        }


        public void Quantidade()
        {
            if (Tipo == Enums.TipoTransacao.Compra)
            {
                Saldo.SaldoAtualRS -= Valor;
                CriptoSaldo.Quantidade += Valor / CriptomoedaHoje.Valor; 
            }
            if (Tipo == Enums.TipoTransacao.Venda)
            {
                Saldo.SaldoAtualRS += Valor;
                CriptoSaldo.Quantidade -= Valor / CriptomoedaHoje.Valor;
            }
            if (Tipo == Enums.TipoTransacao.Transferencia)
            {
                Saldo.SaldoAtualRS -= Valor;
            }
        }
    }
}
