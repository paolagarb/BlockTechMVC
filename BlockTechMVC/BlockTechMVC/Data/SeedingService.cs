using BlockTechMVC.Models;
using BlockTechMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Data
{
    public class SeedingService
    {
        private ApplicationDbContext _context;

        public SeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.ApplicationUser.Any()
             || _context.Conta.Any()
             || _context.ContaCliente.Any()
             || _context.Criptomoeda.Any()
             || _context.CriptomoedaHoje.Any()
             || _context.Saldo.Any()
             || _context.Transacao.Any())
            {
                return;
            }

            Criptomoeda c1 = new Criptomoeda(1, "Bitcoin", "BTC");
            Criptomoeda c2 = new Criptomoeda(2, "EOS", "EOS");
            Criptomoeda c3 = new Criptomoeda(3, "Ethereum", "ETH");
            Criptomoeda c4 = new Criptomoeda(4, "Stellar", "XLM");
            Criptomoeda c5 = new Criptomoeda(5, "XRP", "XRP");
            Criptomoeda c6 = new Criptomoeda(6, "Cardano", "ADA");

            CriptomoedaHoje ch1 = new CriptomoedaHoje(1, DateTime.Now, 64221.88, c1);
            CriptomoedaHoje ch2 = new CriptomoedaHoje(2, DateTime.Now, 14.63, c2);
            CriptomoedaHoje ch3 = new CriptomoedaHoje(3, DateTime.Now, 2113.99, c3);
            CriptomoedaHoje ch4 = new CriptomoedaHoje(4, DateTime.Now, 0.41, c4);
            CriptomoedaHoje ch5 = new CriptomoedaHoje(5, DateTime.Now, 1.38, c5);
            CriptomoedaHoje ch6 = new CriptomoedaHoje(6, DateTime.Now, 0.59, c6);

            ApplicationUser u1 = new ApplicationUser("3", "paolareg@hotmail.com", "Administrador", "10110110110", "09870080", "SP", "São Paulo", "Rua Vergueiro", "1211", "11954551863", "Teste!!12");

            Conta ct1 = new Conta(1, "Nubank", "0001", "0000.00", TipoConta.ContaCorrente, u1.Nome);

            ContaCliente cc1 = new ContaCliente(1, "012342-12", DateTime.Now, u1);

            Transacao t1 = new Transacao(1, TipoTransacao.Compra, DateTime.Now, 500, ch6, cc1);

            Saldo s1 = new Saldo(1, 1000, 0);
            s1.AddTransacao(t1);

            _context.Criptomoeda.AddRange(c1, c2, c3, c4, c5, c6);
            _context.CriptomoedaHoje.AddRange(ch1, ch2, ch3, ch4, ch5, ch6);
            _context.ApplicationUser.AddRange(u1);
            _context.Conta.AddRange(ct1);
            _context.ContaCliente.AddRange(cc1);
            _context.Transacao.AddRange(t1);
            _context.Saldo.AddRange(s1);
        }
    }
}
