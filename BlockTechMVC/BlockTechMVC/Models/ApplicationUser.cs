using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Documento { get; set; } //CPF/CNPJ
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; } //NumeroCasa
        public string Telefone { get; set; }
    }
}
