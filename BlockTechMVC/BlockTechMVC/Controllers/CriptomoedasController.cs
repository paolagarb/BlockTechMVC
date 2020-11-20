using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlockTechMVC.Data;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace BlockTechMVC.Controllers
{
    //Controller apenas para ADM adicionar, remover e alterar Criptomoedas (nome e símbolo)

    [Authorize(Roles = "Admin")]
    public class CriptomoedasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("criptomoedas")]
        // GET: Criptomoedas
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm =  sortOrder == "Nome" ? "Nome_desc" : "Nome";

            var criptomoedas = from c in _context.Criptomoeda
                               select c;

            switch (sortOrder)
            {
                case "Nome_desc":
                    criptomoedas = criptomoedas.OrderByDescending(s => s.Nome);
                    break;    
                default:
                    criptomoedas = criptomoedas.OrderBy(s => s.Nome);
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                criptomoedas = criptomoedas.Where(c => c.Nome.Contains(searchString));
            }

            return View(await criptomoedas.ToListAsync());
        }
     
        [Route("criptomoedas/detalhes")]
        // GET: Criptomoedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            var criptomoeda = await _context.Criptomoeda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criptomoeda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            return View(criptomoeda);
        }

        [Route("criptomoedas/adicionar")]
        // GET: Criptomoedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Criptomoedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (_context.Criptomoeda.Any(c => c.Nome == criptomoeda.Nome)) return RedirectToAction(nameof(Error), new { message = "Criptomoeda já cadastrada!" });
            if (_context.Criptomoeda.Any(c => c.Simbolo == criptomoeda.Simbolo)) return RedirectToAction(nameof(Error), new { message = "Criptomoeda já cadastrada!" });

            if (ModelState.IsValid)
            {
                _context.Add(criptomoeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criptomoeda);
        }

        [Route("criptomoedas/editar")]
        // GET: Criptomoedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            var criptomoeda = await _context.Criptomoeda.FindAsync(id);
            if (criptomoeda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }
            return View(criptomoeda);
        }

        [Route("criptomoedas/editar")]
        // POST: Criptomoedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (id != criptomoeda.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criptomoeda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriptomoedaExists(criptomoeda.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Error), new { message = "" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(criptomoeda);
        }

        private bool CriptomoedaExists(int id)
        {
            return _context.Criptomoeda.Any(e => e.Id == id);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            };

            return View(viewModel);
        }
    }

}
