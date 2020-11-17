using BlockTechMVC.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlockTechMVC.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Banco { get; set; }

        [StringLength(4, ErrorMessage = "Número Inválido.")]

        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Display(Name ="Número da Conta")]
        public string NumeroConta { get; set; }

        [Display(Name ="Tipo")]
        public TipoConta TipoConta { get; set; }

        public Conta()
        {

        }

        public Conta(string banco, string agencia, string numeroConta, TipoConta tipoConta)
        {
            Banco = banco;
            Agencia = agencia;
            NumeroConta = numeroConta;
            TipoConta = tipoConta;
        }
    }
}
