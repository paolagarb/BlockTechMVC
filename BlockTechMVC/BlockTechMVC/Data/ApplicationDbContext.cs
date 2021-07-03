using BlockTechMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlockTechMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<Criptomoeda> Criptomoeda { get; set; }
        public DbSet<CriptomoedaHoje> CriptomoedaHoje { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; } 
        public DbSet<Conta> Conta { get; set; }
        public DbSet<ContaCliente> ContaCliente { get; set; }
        public DbSet<Saldo> Saldo { get; set; }
        public DbSet<CriptoSaldo> CriptoSaldo { get; set; }
    }
}
