using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;

namespace BlockTechMVC.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly ApplicationDbContext _context;

        public ContaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Saldo> Listar(string user)
        {
           return _context.Saldo
                 .Include(t => t.ContaCliente)
                 .Include(t => t.ContaCliente.ApplicationUser)
                 .Where(t => t.ContaCliente.ApplicationUser.UserName == user).ToList();
        }

        public List<Saldo> ListarContains(string user)
        {
           return _context.Saldo
                .Include(t => t.ContaCliente)
                .Include(t => t.ContaCliente.ApplicationUser)
                .Where(t => t.ContaCliente.ApplicationUser.Nome.Contains(user)).ToList();
        }

        public List<string> ListarUsuarios()
        {
            List<string> usuarios = new List<string>();

            foreach (var item in _context.Saldo)
            {
                var usuario = (from c in _context.Saldo
                               join conta in _context.ContaCliente
                               on c.ContaClienteId equals conta.Id
                               join cliente in _context.ApplicationUser
                               on conta.ApplicationUserID equals cliente.Id
                               where item.ContaClienteId == conta.Id
                               select cliente.UserName).FirstOrDefault();

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public string CarregarUsuario(int? id)
        {
            return (from c in _context.Saldo
                    join conta in _context.ContaCliente
                    on c.ContaClienteId equals conta.Id
                    join cliente in _context.ApplicationUser
                    on conta.ApplicationUserID equals cliente.Id
                    where c.Id == id
                    select cliente.UserName).FirstOrDefault();
        }

        IIncludableQueryable<Saldo, ApplicationUser> IContaRepository.CarregarAplicacoes()
        {
            return _context.Saldo
                  .Include(t => t.ContaCliente)
                  .Include(t => t.ContaCliente.ApplicationUser);
        }

        public Transacao CarregarTransacao(int? id)
        {
            return  _context.Transacao
                    .Include(t => t.ContaCliente)
                    .Include(t => t.CriptoSaldo)
                    .Include(t => t.CriptomoedaHoje)
                    .Include(t => t.ContaCliente.Conta)
                    .Include(t => t.ContaCliente.ApplicationUser)
                    .Include(t => t.Saldo)
                    .FirstOrDefault(m => m.Saldo.Id == id);
        }
    }
}
