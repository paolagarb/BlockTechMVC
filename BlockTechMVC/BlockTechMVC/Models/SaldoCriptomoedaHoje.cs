using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class SaldoCriptomoedaHoje
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public CompraCriptomoeda CompraCriptomoeda {get;set;}
        public int CompraCriptomoedaId { get; set; }

        public SaldoCriptomoedaHoje()
        {

        }

        public SaldoCriptomoedaHoje(int id, DateTime data, CompraCriptomoeda compraCriptomoeda, int compraCriptomoedaId)
        {
            Id = id;
            Data = data;
            CompraCriptomoeda = compraCriptomoeda;
            CompraCriptomoedaId = compraCriptomoedaId;
        }

        public double CalcularSaldoHoje(CompraCriptomoeda compraCriptomoeda)
        {
            return compraCriptomoeda.ValorAplicado * compraCriptomoeda.CalcularQuantidadeCriptomoeda();
        }
    }
}
