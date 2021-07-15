using BlockTechMVC.Models.MercadoBitcoin;
using System;
using System.Threading.Tasks;

namespace BlockTechMVC.Interfaces
{
    public interface ICriptomoedaMercadoBitcoinFacade
    {
        public Task<DaySummaryMBResponse> ObterValor(string criptomoeda, DateTime data);
        public Task<TickerMBResponse> ObterValorAtual(string criptomoeda);
    }
}
