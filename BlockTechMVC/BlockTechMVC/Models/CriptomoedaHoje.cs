using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class CriptomoedaHoje
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(20,2)")]
        public double Valor { get; set; }
        public virtual Criptomoeda Criptomoeda { get; set; }            ///
        public int CriptomoedaId { get; set; }

        public CriptomoedaHoje()
        {

        }

        public CriptomoedaHoje(DateTime data, double valor, Criptomoeda criptomoeda)
        {
            Data = data;
            Valor = valor;
            Criptomoeda = criptomoeda;
        }
    }
}
