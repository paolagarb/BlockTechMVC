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
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Bitcoin");

            return View();
        }

        public ActionResult Ethereum()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Ethereum");

            return View();
        }

        public ActionResult BitcoinCash()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Bitcoin Cash");

            return View();
        }

        public ActionResult Xrp()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("XRP");

            return View();
        }

        public ActionResult PaxGold()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("PAX Gold");

            return View();
        }

        public ActionResult Litecoin()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Valores = Valores7Dias("Litecoin");

            return View();
        }

        public List<int> Ultimos7Dias()
        {
            var dias = 7;
            var diasList = new List<int>();

            for (int i = 0; i < dias; i++)
            {
                diasList.Add(DateTime.Now.Day - i);
            }

            return diasList;
        }

        public List<double> Valores7Dias(string nome)
        {
            var dias = 7;
            var valorList = new List<double>();

            for (int i = 0; i < dias; i++)
            {
                var data = DateTime.Now.Day - i;
                DateTime dia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, data);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Equals(dia)
                             select criptohoje.Valor).Single();
                valorList.Add(valor);
            }

            return valorList;
        }

      

    }
}
