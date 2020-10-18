using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class ContaCliente
    {
        public int Id { get; set; }
        public string NumeroConta { get; set; }
        public DateTime DataAbertura { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        // public List<Conta> Conta { get; set; }
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

        //public void AddConta(Conta conta)
        //{
        //    Conta.Add(conta);
        //}
    }
}
