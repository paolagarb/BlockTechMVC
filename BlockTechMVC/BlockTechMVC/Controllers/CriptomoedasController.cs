using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlockTechMVC.Controllers
{
    public class CriptomoedasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
