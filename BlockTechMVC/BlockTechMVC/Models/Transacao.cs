using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Transacao
    {
        public int Id { get; set; }

        [Display(Name ="Tipo de Transação")]
        public TipoTransacao Tipo { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int CriptomoedaHojeId { get; set; }
        public CriptomoedaHoje CriptomoedaHoje { get; set; }
        public int ContaClienteId { get; set; }
        public ContaCliente ContaCliente { get; set; }

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

        //public Transacao(Tipotransacao tipo, datetime data, double valor, Criptomoedahoje criptomoedahoje, contacliente contacliente, conta contadestino) : this(tipo, data, valor, criptomoedahoje, contacliente)
        //{
        //    contadestino = contadestino;
        //}

        public double CalcularQuantidadeCripto()
        {
            return Valor / CriptomoedaHoje.Valor;
        }
    }
}
