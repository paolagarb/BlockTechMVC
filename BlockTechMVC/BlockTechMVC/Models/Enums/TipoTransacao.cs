using System.ComponentModel;

namespace BlockTechMVC.Models.Enums
{
    public enum TipoTransacao : int
    {
        [Description("Transferencia")]
        Transferencia = 0,
        [Description("Venda")]
        Venda = 1,
        [Description("Compra")]
        Compra = 2
    }
}
