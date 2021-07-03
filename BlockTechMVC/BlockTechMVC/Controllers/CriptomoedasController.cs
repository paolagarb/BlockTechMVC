using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlockTechMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CriptomoedasController : Controller
    {
        private readonly ICriptomoedaRepository _repository;

        public CriptomoedasController(ICriptomoedaRepository repository)
        {
            _repository = repository;
        }

        [Route("criptomoedas")]
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            try
            {
                var criptomoedas = await _repository.Carregar(searchString, sortOrder);

                ViewBag.NameSortParm = sortOrder == "Nome" ? "Nome_desc" : "Nome";

                return View(criptomoedas);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado." });
            }
        }

        [Route("criptomoedas/detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });

            var criptomoeda = await _repository.Detalhes((int)id);

            if (criptomoeda == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });

            return View(criptomoeda);
        }

        [Route("criptomoedas/adicionar")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (_repository.ConsultarCriptomoedaNome(criptomoeda.Nome) || _repository.ConsultarCriptomoedaSimbolo(criptomoeda.Simbolo))
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda já cadastrada!" });

            if (ModelState.IsValid)
            {
                await _repository.Adicionar(criptomoeda);

                return RedirectToAction(nameof(Index));
            }

            return View(criptomoeda);
        }

        [Route("criptomoedas/editar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });

            var criptomoeda = await _repository.Carregar(id);

            if (criptomoeda == null)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });

            return View(criptomoeda);
        }

        [Route("criptomoedas/editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo,Cadastro")] Criptomoeda criptomoeda)
        {
            if (id != criptomoeda.Id)
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado!" });

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Editar(criptomoeda);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.Carregar(id) != null)
                        return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
                    else
                        return RedirectToAction(nameof(Error), new { message = "" });
                }

                return RedirectToAction(nameof(Index));
            }

            return View(criptomoeda);
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
