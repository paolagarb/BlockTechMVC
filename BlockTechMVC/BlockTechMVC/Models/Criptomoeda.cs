using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Criptomoeda
    {
        public int Id { get; set; }

        [Display(Name ="Nome da Criptomoeda")]
        public string Nome { get; set; }

        [Display(Name = "Símbolo da Criptomoeda")]
        public string Simbolo { get; set; }

        [Display(Name ="Data de Cadastro")]
        public DateTime Cadastro { get; set; }

        public Criptomoeda()
        {

        }

        public Criptomoeda(string nome, string simbolo)
        {
            Nome = nome;
            Simbolo = simbolo;
            Cadastro = DateTime.Now;
        }
    }
}
