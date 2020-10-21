using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class CriptoSaldo
    {
        public int Id { get; set; }
        public string Criptomoeda { get; set; } //Nome

        [Display(Name = "Saldo em Criptomoeda")]
        [Column(TypeName = "decimal(20,2)")]
        public double Quantidade { get; set; } //Quantidade de Criptomoedas

    }
}
