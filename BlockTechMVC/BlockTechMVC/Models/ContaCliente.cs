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


        [Required(ErrorMessage = "Insira o número da conta!", AllowEmptyStrings = false)]
        [Display(Name="Número da Conta")]
        //[DisplayFormat(DataFormatString = "{{{0:###-##}}}")]
        [DisplayFormat(DataFormatString = "{0:###-##}")]
        public int NumeroConta { get; set; } //IDENTITY

        [Display(Name ="Data de Abertura")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAbertura { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserID { get; set; }

        public virtual Conta Conta { get; set; }
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
