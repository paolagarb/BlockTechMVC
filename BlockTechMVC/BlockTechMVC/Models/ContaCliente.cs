using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class ContaCliente
    {
        public int Id { get; set; }
        
        [Display(Name="Número da Conta")]
        public string NumeroConta { get; set; } //IDENTITY

        [Display(Name ="Data de Abertura")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataAbertura { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Conta Conta { get; set; }
        public int ContaId { get; set; }
        public ContaCliente()
        {

        }
        
        public ContaCliente(DateTime dataAbertura, ApplicationUser applicationUser, Conta conta)
        {
            DataAbertura = dataAbertura;
            ApplicationUser = applicationUser;
            Conta = conta;
        }
    }
}
