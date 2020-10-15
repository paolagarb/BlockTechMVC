using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<BlockTechMVC.Models.Transacao> Transacao { get; set; }
        public DbSet<BlockTechMVC.Models.Criptomoeda> Criptomoeda { get; set; }
        public DbSet<BlockTechMVC.Models.CriptomoedaHoje> CriptomoedaHoje { get; set; }
    }
}
