﻿using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
    }
}
