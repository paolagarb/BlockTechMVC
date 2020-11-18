using System.ComponentModel;

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