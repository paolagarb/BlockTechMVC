using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Models.Enums
{
    public enum Transacao : int
    {
        Transferencia = 0,
        Venda = 1,
        Compra = 2
    }
}
