using BlockTechMVC.Models;
using BlockTechMVC.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
            //
            //    if (_context.ApplicationUser.Any()
            //     || _context.Conta.Any()
            //     || _context.ContaCliente.Any()
            //     || _context.Criptomoeda.Any()
            //     || _context.CriptomoedaHoje.Any()
            //     || _context.Saldo.Any()
            //     || _context.Transacao.Any())
            //    {
            //        return;
            //    }

            //    Criptomoeda c1 = new Criptomoeda("Bitcoin", "BTC");
            //    Criptomoeda c2 = new Criptomoeda("EOS", "EOS");
            //    Criptomoeda c3 = new Criptomoeda("Ethereum", "ETH");
            //    Criptomoeda c4 = new Criptomoeda("Stellar", "XLM");
            //    Criptomoeda c5 = new Criptomoeda("XRP", "XRP");
            //    Criptomoeda c6 = new Criptomoeda("Cardano", "ADA");

            //    CriptomoedaHoje ch1 = new CriptomoedaHoje(DateTime.Now, 64221.88, c1);
            //    CriptomoedaHoje ch2 = new CriptomoedaHoje(DateTime.Now, 14.63, c2);
            //    CriptomoedaHoje ch3 = new CriptomoedaHoje(DateTime.Now, 2113.99, c3);
            //    CriptomoedaHoje ch4 = new CriptomoedaHoje(DateTime.Now, 0.41, c4);
            //    CriptomoedaHoje ch5 = new CriptomoedaHoje(DateTime.Now, 1.38, c5);
            //    CriptomoedaHoje ch6 = new CriptomoedaHoje(DateTime.Now, 0.59, c6);

            //    ApplicationUser u1 = new ApplicationUser("paolareg@hotmail.com", "Administrador", "10110110110", "09870080", "SP", "São Paulo", "Rua Vergueiro", "1211", "11954551863", "Teste!!12");

            //    Conta ct1 = new Conta("Nubank", "0001", "0000.00", TipoConta.ContaCorrente, u1.Nome);

            //    ContaCliente cc1 = new ContaCliente("012342-12", DateTime.Now, u1, ct1);

            //    Transacao t1 = new Transacao(TipoTransacao.Compra, DateTime.Now, 500, ch6, cc1);

            //    Saldo s1 = new Saldo(1000, 0, t1);

            //    _context.Criptomoeda.AddRange(c1, c2, c3, c4, c5, c6);
            //    _context.CriptomoedaHoje.AddRange(ch1, ch2, ch3, ch4, ch5, ch6);
            //    _context.ApplicationUser.AddRange(u1);
            //    _context.Conta.AddRange(ct1);
            //    _context.ContaCliente.AddRange(cc1);
            //    _context.Transacao.AddRange(t1);
            //    _context.Saldo.AddRange(s1);

            //    _context.SaveChanges();
        }
    }
}
