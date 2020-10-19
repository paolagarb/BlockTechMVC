﻿using Microsoft.JSInterop;
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
        public string NumeroConta { get; set; }

        [Display(Name ="Data de Abertura")]
        public DateTime DataAbertura { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Conta Conta { get; set; }
        public int ContaId { get; set; }
        public ContaCliente()
        {

        }
        
        public ContaCliente(string numeroConta, DateTime dataAbertura, ApplicationUser applicationUser, Conta conta)
        {
            NumeroConta = numeroConta;
            DataAbertura = dataAbertura;
            ApplicationUser = applicationUser;
            Conta = conta;
        }
    }
}
