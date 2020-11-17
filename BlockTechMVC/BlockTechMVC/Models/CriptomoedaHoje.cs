using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockTechMVC.Models
{
    public class CriptomoedaHoje
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(20,2)")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Valor { get; set; }

        public Criptomoeda Criptomoeda { get; set; }  
        
        [Display(Name ="Criptomoeda")]
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
