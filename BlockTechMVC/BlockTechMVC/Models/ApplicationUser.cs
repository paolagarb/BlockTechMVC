using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; } //Razão Social ou Nome
        public string Documento { get; set; } //CPF ou CNPJ
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; } //Número da casa 
        public string Telefone { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string id, string email, string nome, string documento, string cep, string uf, string cidade, string rua, string numero, string telefone, string password)
        {
            Id = id;
            Email = email;
            Nome = nome;
            Documento = documento;
            Cep = cep;
            Uf = uf;
            Cidade = cidade;
            Rua = rua;
            Numero = numero;
            Telefone = telefone;
            PasswordHash = password;
        }
    }
}
