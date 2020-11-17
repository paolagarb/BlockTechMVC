using System;
using System.ComponentModel.DataAnnotations;

namespace BlockTechMVC.Models
{
    public class Criptomoeda
    {
        public int Id { get; set; }

        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [Display(Name = "Símbolo")]
        [StringLength(5, ErrorMessage = "Esse símbolo não existe.")]
        public string Simbolo { get; set; }

        [Display(Name ="Data de Cadastro")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Cadastro { get; set; }

        public Criptomoeda()
        {

        }

        public Criptomoeda(string nome, string simbolo, DateTime cadastro)
        {
            Nome = nome;
            Simbolo = simbolo;
            Cadastro = cadastro;
        }
    }
}
