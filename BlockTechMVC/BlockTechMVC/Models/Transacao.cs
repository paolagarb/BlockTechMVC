using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public Transacao Tipo { get; set; }
    }
}
