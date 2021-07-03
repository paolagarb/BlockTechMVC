using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface IContaRepository
    {
        List<Saldo> Listar(string user);
        List<Saldo> ListarContains(string user);
        List<string> ListarUsuarios();
        string CarregarUsuario(int? id);
        IIncludableQueryable<Saldo, ApplicationUser> CarregarAplicacoes();
        Transacao CarregarTransacao(int? id);
    }
}
