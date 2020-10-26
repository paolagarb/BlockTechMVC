using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome/Razão Social")]
        [StringLength(60)]
        public string Nome { get; set; } //Razão Social ou Nome

        [StringLength(14, ErrorMessage = "CNPJ Inexistente.")]
        [Display(Name = "CPF/CNPJ")]
        public string Documento { get; set; } //CPF ou CNPJ

        [StringLength(9, ErrorMessage = "CEP Inválido.")]
        public string Cep { get; set; }

        [StringLength(2, ErrorMessage = "UF Inválida.")]
        public string Uf { get; set; }

        [StringLength(58, ErrorMessage = "Essa cidade não existe.")] //O maior nome de cidade possui 58 caracteres
        public string Cidade { get; set; }
        public string Rua { get; set; }

        [StringLength(10)]
        public string Numero { get; set; } //Número da casa 

        [StringLength(15, ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string email, string nome, string documento, string cep, string uf, string cidade, string rua, string numero, string telefone, string password)
        {
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
