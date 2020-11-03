using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;
using System.ComponentModel;

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
            ViewBag.Bitcoin = CriptomoedaHoje("Bitcoin").ToString("F4");
            ViewBag.Ethereum = CriptomoedaHoje("Ethereum").ToString("F4");
            ViewBag.BitcoinCash = CriptomoedaHoje("Bitcoin Cash").ToString("F4");
            ViewBag.XRP = CriptomoedaHoje("XRP").ToString("F4");
            ViewBag.PaxGold = CriptomoedaHoje("PAX Gold").ToString("F4");
            ViewBag.Litecoin = CriptomoedaHoje("Litecoin").ToString("F4");

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
            var diasList = new List<int>();

            DateTime diasSete = DateTime.Today;
            diasSete = diasSete.AddDays(-7);

            for (int i = 1; i <= 7; i++)
            {

                diasList.Add((diasSete.Day) + i);
            }

            return diasList;

            //var diasList = new List<int>();

            //DateTime diasSete = DateTime.Now;
            //diasSete = diasSete.AddDays(-7);

            //for (int i = 1; i <= 7; i++)
            //{
            //    diasSete = diasSete.AddDays(i);
            //    diasList.Add(diasSete.Day);
            //}

            //return diasList;

        }

        public List<double> Valores7Dias(string nome)
        {
         
            var valorList = new List<double>();
          
            for (int i = 1; i <= 7; i++)
            {
                DateTime diasSete = DateTime.Today;
                diasSete = diasSete.AddDays(-7);
                DateTime data = diasSete.AddDays(i);

                var valor = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(data.Date)
                             select criptohoje.Valor).Single();
                valorList.Add(valor);
            }

            return valorList;
        }

        public IActionResult Semanal()
        {
            ViewBag.Dias = Ultimos7Dias();
            ViewBag.Bitcoin = Porcentagem("Bitcoin");
            ViewBag.Ethereum = Porcentagem("Ethereum");
            ViewBag.BitcoinCash = Porcentagem("Bitcoin Cash");
            ViewBag.XRP = Porcentagem("XRP");
            ViewBag.PaxGold = Porcentagem("PAX Gold");
            ViewBag.Litecoin = Porcentagem("Litecoin");
            return View();
        }

        public List<double> Porcentagem(string nome)
        {
            //var dia = DateTime.Now.Day - 7;
            //DateTime dateSeteDias = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dia);

            var dia = DateTime.Today;
            dia = dia.AddDays(-7);
            DateTime dateSeteDias = dia;

            var valor = (from coin in _context.Criptomoeda
                         join criptohoje in _context.CriptomoedaHoje
                         on coin.Id equals criptohoje.CriptomoedaId
                         where coin.Nome == nome && criptohoje.Data.Date.Equals(dateSeteDias.Date)
                         select criptohoje.Valor).Single();

            var calculo = 100.0; //inicia com 100%
            var porcentagem = new List<double>();

            porcentagem.Add(0);

            for (int i = 1; i < 7; i++)
            {
                DateTime data = dia.AddDays(i);
                DateTime date = data;

                var valorDia = (from coin in _context.Criptomoeda
                             join criptohoje in _context.CriptomoedaHoje
                             on coin.Id equals criptohoje.CriptomoedaId
                             where coin.Nome == nome && criptohoje.Data.Date.Equals(date.Date)
                             select criptohoje.Valor).Single();
                var regra3 = ((valorDia * calculo) / valor).ToString("F2");
                calculo = Convert.ToDouble(regra3);
               valor = calculo;
                var resultado = calculo - 100;
                porcentagem.Add(resultado);
            }

            return porcentagem;
        }
    }
}
