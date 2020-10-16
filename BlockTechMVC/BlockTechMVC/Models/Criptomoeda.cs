﻿using System;
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
