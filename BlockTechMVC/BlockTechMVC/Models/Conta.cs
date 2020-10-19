using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }

        [Display(Name ="Número da Conta")]
        public string NumeroConta { get; set; }

        [Display(Name ="Tipo de Conta")]
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
