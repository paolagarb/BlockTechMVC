using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlockTechMVC.Controllers
{
    public class CriptomoedasHojeController : Controller
    {
        public IActionResult Index()
        {
            List<CriptomoedaHoje> list = new List<CriptomoedaHoje>();
            list.Add(new CriptomoedaHoje
            {
                Id = 1,
                Data = DateTime.Now,
                Valor = 63370.61,
                Criptomoeda = new Criptomoeda { Id = 1, Nome = "Bitcoin", Simbolo = "BTC"}
            });
            list.Add(new CriptomoedaHoje
            {
                Id = 2,
                Data = DateTime.Now,
                Valor = 2094.65,
                Criptomoeda = new Criptomoeda { Id = 2, Nome = "Ethereum", Simbolo = "ETH" }
            });


            return View(list);
        }
    }
}
