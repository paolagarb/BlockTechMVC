using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface IRelatorioRepository
    {
        List<double> SaldoUltimosDias(int quantidadeDias, string cripto);
        double ValorAtualCriptomoeda(string cripto);
        List<double> PorcentualUltimosDias(int quantidadeDias, string cripto);
    }
}
