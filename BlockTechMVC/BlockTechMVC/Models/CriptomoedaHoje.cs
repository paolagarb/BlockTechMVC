﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class CriptomoedaHoje
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public Criptomoeda Criptomoeda { get; set; }
        public int CriptomoedaId { get; set; }
    }
}
