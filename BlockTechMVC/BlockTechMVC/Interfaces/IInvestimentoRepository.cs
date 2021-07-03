using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface IInvestimentoRepository
    {
        List<double> ValorTotalUltimosDias(int quantidadeDias, double totalCripto, string cripto);
        List<double> ValorTotalUltimosDiasAdm(int quantidadeDias, string cripto, double totalCripto);
        double ValorInvestido(string cripto, string user);
        double ValorInvestidoAdm(string cripto);
        int QuantidadeCriptomoedaAdm(string cripto);
        double ValorCriptomoedaAdm(string cripto, double quantidade);
        int PrimeiroInvestimento(string cripto, string user);
        int PrimeiroInvestimentoAd(string cripto);
    }
}
