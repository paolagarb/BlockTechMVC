using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public string TipoConta { get; set; }
        public string NomeDestinatario { get; set; }

        public Conta()
        {

        }

        public Conta(int id, string banco, string agencia, string numeroConta, string tipoConta, string nomeDestinatario)
        {
            Id = id;
            Banco = banco;
            Agencia = agencia;
            NumeroConta = numeroConta;
            TipoConta = tipoConta;
            NomeDestinatario = nomeDestinatario;
        }
    }
}
