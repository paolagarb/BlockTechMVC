using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;

namespace BlockTechMVC.Controllers
{
    public class Transacoes : Controller
    {
        public IActionResult Index()
        {
            List<Transacao> list = new List<Transacao>();
            list.Add(new Transacao { Id = 1, Tipo = (Models.Enums.TipoTransacao)1, Data = DateTime.Now, Valor = 0 });
            list.Add(new Transacao { Id = 2, Tipo = (Models.Enums.TipoTransacao)1, Data = DateTime.Now, Valor = 0 }); ;
            return View(list);
        }
    }
}
