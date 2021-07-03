using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace BlockTechMVC.Repositories
{
    public class AplicacaoRepository : IAplicacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public AplicacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CriptoSaldo> CarregarSaldoPorUser(string user)
        {
            return _context.CriptoSaldo
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user)
                    .Include(t => t.ContaCliente)
                    .Where(t => t.ContaCliente.ApplicationUser.UserName == user).ToList();
        }

        public List<CriptoSaldo> CarregarSaldoPorCripto(string user, string cripto)
        {
            return _context.CriptoSaldo
                    .Include(t => t.ContaCliente)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Where(t => t.Criptomoeda.Contains(cripto) && (t.ContaCliente.ApplicationUser.UserName == user))
                    .ToList();
        }

        public IIncludableQueryable<CriptoSaldo, ApplicationUser> CarregarSaldo()
        {
            return _context.CriptoSaldo
                    .Include(t => t.ContaCliente)
                    .Include(t => t.ContaCliente.ApplicationUser);
        }

        public List<CriptoSaldo> CarregarSaldoPorCriptoContain(string cripto)
        {
            return _context.CriptoSaldo
                    .Include(t => t.ContaCliente)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Where(t => t.Criptomoeda.Contains(cripto)).ToList();
        }

        public List<CriptoSaldo> CarregarSaldoPorUserContain(string user)
        {
            return _context.CriptoSaldo
                   .Include(t => t.ContaCliente)
                   .Include(t => t.ContaCliente.ApplicationUser)
                   .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(user)).ToList();
        }
    }
}
