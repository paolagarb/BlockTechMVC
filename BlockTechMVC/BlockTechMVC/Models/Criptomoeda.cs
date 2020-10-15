using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Criptomoeda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Simbolo { get; set; }

        public Criptomoeda()
        {

        }

        public Criptomoeda(int id, string nome, string simbolo)
        {
            Id = id;
            Nome = nome;
            Simbolo = simbolo;
        }
    }
}
