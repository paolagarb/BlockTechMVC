using BlockTechMVC.Models;
using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface ITransacaoRepository
    {
        List<Transacao> Carregar();
        List<Transacao> Carregar(string user);
        List<Transacao> CarregarPorUsuario(string user);
        List<Transacao> CarregarPorCriptomoedaAdm(string cripto);
        List<Transacao> CarregarPorCriptomoeda(string cripto, string user);
    }
}
