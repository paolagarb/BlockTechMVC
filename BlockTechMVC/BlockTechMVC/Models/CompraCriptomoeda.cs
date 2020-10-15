using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class CompraCriptomoeda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public ContaCliente ContaCliente{ get; set; }
        public int ContaClienteId { get; set; }
        public CriptomoedaHoje CriptomoedaHoje { get; set; }
        public int CriptomoedaHojeId { get; set; }
        public int ValorAplicado { get; set; } //Valor aplicado na criptomoeda
        public double QuantidadeCriptomoeda { get; set; }

        public CompraCriptomoeda()
        {

        }

        public CompraCriptomoeda(int id, DateTime data, ContaCliente contaCliente, int contaClienteId, CriptomoedaHoje criptomoedaHoje, int criptomoedaHojeId, int valorAplicado)
        {
            Id = id;
            Data = data;
            ContaCliente = contaCliente;
            ContaClienteId = contaClienteId;
            CriptomoedaHoje = criptomoedaHoje;
            CriptomoedaHojeId = criptomoedaHojeId;
            ValorAplicado = valorAplicado;
            QuantidadeCriptomoeda = valorAplicado / criptomoedaHoje.Valor;
        }

    }
}
