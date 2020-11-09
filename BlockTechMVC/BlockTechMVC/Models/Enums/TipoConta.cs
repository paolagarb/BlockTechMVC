using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models.Enums
{
    public enum TipoConta : int
    {
        [Description("Conta Corrente")]
        ContaCorrente = 0,
        [Description("Conta Poupança")]
        ContaPoupanca = 1,
    }
}
