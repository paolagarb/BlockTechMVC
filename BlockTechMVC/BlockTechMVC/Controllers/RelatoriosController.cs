using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;

namespace BlockTechMVC.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Relatorios
        public ActionResult Index()
        {
            ViewBag.Bitcoin = CriptomoedaHoje("Bitcoin");
            ViewBag.Ethereum = CriptomoedaHoje("Ethereum");
            ViewBag.BitcoinCash = CriptomoedaHoje("Bitcoin Cash");
            ViewBag.XRP = CriptomoedaHoje("XRP");
            ViewBag.PaxGold = CriptomoedaHoje("PAX Gold");
            ViewBag.Litecoin = CriptomoedaHoje("Litecoin");

            return View();
        }

        public double CriptomoedaHoje(string nome)
        {
            var criptomoeda = (from coin in _context.Criptomoeda
                               join criptohoje in _context.CriptomoedaHoje
                               on coin.Id equals criptohoje.CriptomoedaId
                               where coin.Nome == nome && criptohoje.Data == DateTime.Today
                               select criptohoje.Valor).Single();

            return criptomoeda;
        }

        public ActionResult Bitcoin()
        {
            var dias = 7;

            var diasList = new List<int>();
            for (int i = 0; i < dias; i++)
            {
                diasList.Add(DateTime.Now.Day - i);
            }

            ViewBag.Dias = diasList;

            var valorList = new List<double>();
            for (int i = 0; i < dias; i++)
            {
                var data = DateTime.Now.Day - i;

                DateTime dia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, data);

                var valor = (from coin in _context.Criptomoeda
                                   join criptohoje in _context.CriptomoedaHoje
                                   on coin.Id equals criptohoje.CriptomoedaId
                                   where coin.Nome == "Bitcoin" && criptohoje.Data.Equals(dia)
                                   select criptohoje.Valor).Single();
                valorList.Add(valor);
            }

            ViewBag.Valores = valorList;

            return View();
        }

    }
}
