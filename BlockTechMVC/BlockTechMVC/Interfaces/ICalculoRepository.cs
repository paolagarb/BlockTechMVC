namespace BlockTechMVC.Interfaces
{
    public interface ICalculoRepository
    {
        double TotalNaoInvestido(string user);
        double TotalCriptomoeda(string criptomoeda, string user);
        double SaldoAtual(double quantidadeCriptomoeda, string criptomoeda);
    }
}
