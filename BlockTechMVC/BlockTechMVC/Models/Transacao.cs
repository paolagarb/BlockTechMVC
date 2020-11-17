using BlockTechMVC.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Valor { get; set; }

        [Display(Name = "Criptomoeda")]
        public int CriptomoedaHojeId { get; set; }
        [Display(Name = "Criptomoeda")]
        public CriptomoedaHoje CriptomoedaHoje { get; set; }

        [Display(Name = "Conta")]
        public int ContaClienteId { get; set; }

        [Display(Name = "Conta")]
        public ContaCliente ContaCliente { get; set; }

        [Display(Name = "Saldo Criptomoeda")]
        public CriptoSaldo CriptoSaldo { get; set; }
        [Display(Name = "Saldo Criptomoeda")]
        public int CriptoSaldoId { get; set; }

        [Display(Name = "Saldo")]
        public int SaldoId { get; set; }
        [Display(Name = "Saldo")]
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
        }
    }
}
