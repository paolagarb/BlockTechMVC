using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlockTechMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome/Razão Social")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use apenas caracteres alfabéticos.")]
        [Required(ErrorMessage = "O campo 'Nome/Razão Social' está vazio.")]
        [StringLength(60)]
        public string Nome { get; set; } //Razão Social ou Nome

        [StringLength(14, ErrorMessage = "CNPJ Inexistente.")]
        [Display(Name = "CPF/CNPJ")]
        [Required(ErrorMessage = "O campo 'Documento' está vazio.")]
        public string Documento { get; set; } //CPF ou CNPJ

        [StringLength(9, ErrorMessage = "CEP Inválido.")]
        [Required(ErrorMessage = "O campo 'CEP' está vazio.")]
        public string Cep { get; set; }

        [StringLength(2, ErrorMessage = "UF Inválida.")]
        [Required(ErrorMessage = "O campo 'UF' está vazio.")]
        public string Uf { get; set; }

        [StringLength(58, ErrorMessage = "Essa cidade não existe.")] //O maior nome de cidade possui 58 caracteres
        [Required(ErrorMessage = "O campo 'Cidade' está vazio.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo 'Rua' está vazio.")]
        public string Rua { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "O campo 'Número' está vazio.")]
        [Display(Name ="Número")]
        public string Numero { get; set; } //Número da casa 

        [Required(ErrorMessage = "O campo 'Telefone' está vazio.")]
        [StringLength(15, ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; }

        public ApplicationUser()
        {

        }

        public ApplicationUser(string email, string nome, string documento, string cep, string uf, string cidade, string rua, string numero, string telefone, string password) : base()
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
