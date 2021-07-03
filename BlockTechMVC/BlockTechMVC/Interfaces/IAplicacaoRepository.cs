using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface IAplicacaoRepository
    {
        IIncludableQueryable<CriptoSaldo, ApplicationUser> CarregarSaldo();
        List<CriptoSaldo> CarregarSaldoPorUserContain(string user);
        List<CriptoSaldo> CarregarSaldoPorCriptoContain(string cripto);
        List<CriptoSaldo> CarregarSaldoPorUser(string user);
        List<CriptoSaldo> CarregarSaldoPorCripto(string user, string cripto);
    }
}
