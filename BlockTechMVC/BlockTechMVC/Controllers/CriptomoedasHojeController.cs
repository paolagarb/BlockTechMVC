using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class CriptomoedasHojeController : Controller
    {
        private readonly ICriptomoedaHojeRepository _repository;

        public CriptomoedasHojeController(ICriptomoedaHojeRepository repository)
        {
            _repository = repository;
        }

        [Route("criptomoedas-valores")]
        public async Task<IActionResult> Index(DateTime searchDate, string sortOrder)
        {
            try
            {
                ViewBag.NameSortParm = sortOrder == "Nome" ? "Nome_desc" : "Nome";
                ViewBag.ValueSortParm = sortOrder == "Valor" ? "Valor_desc" : "Valor";

                var criptomoedas = _repository.Listar();

                if (searchDate != DateTime.MinValue)
                    criptomoedas = _repository.ListarPorData(searchDate);

                if (String.IsNullOrEmpty(sortOrder))
                {
                    var criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);

                    switch (sortOrder)
                    {
                        case "Nome_desc":
                            criptomoeda = criptomoedas.OrderByDescending(c => c.Criptomoeda.Nome);
                            break;
                        case "Valor_desc":
                            criptomoeda = criptomoedas.OrderByDescending(c => c.Valor);
                            break;
                        case "Valor":
                            criptomoeda = criptomoedas.OrderBy(c => c.Valor);
                            break;
                        case "Nome":
                            criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);
                            break;
                        default:
                            criptomoeda = criptomoedas.OrderBy(c => c.Criptomoeda.Nome);
                            break;
                    }

                    return View(criptomoeda);
                }

                return View(criptomoedas);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado. Tente novamente." });
            }
        }

        [Route("criptomoedas-valores/detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });


            var criptomoedaHoje = _repository.Carregar(id);

            if (criptomoedaHoje == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            return View(criptomoedaHoje);
        }

        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/adicionar")]
        public IActionResult Create()
        {
            ViewData["CriptomoedaId"] = new SelectList(_repository.CarregarCriptomoedas(), "Id", "Nome");
            ViewData["CriptomoedaIdSimb"] = new SelectList(_repository.CarregarCriptomoedas(), "Id", "Simbolo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/adicionar")]
        public async Task<IActionResult> Create([Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Adicionar(criptomoedaHoje);

                    return RedirectToAction(nameof(Index));
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/editar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            var criptomoedaHoje = _repository.Carregar(id);

            if (criptomoedaHoje == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            var criptomoedas = _repository.CarregarCriptomoedas();

            ViewData["CriptomoedaId"] = new SelectList(criptomoedas, "Id", "Nome", criptomoedaHoje.CriptomoedaId);
            ViewData["CriptomoedaIdSimb"] = new SelectList(criptomoedas, "Id", "Simbolo");

            return View(criptomoedaHoje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/editar")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Valor,CriptomoedaId")] CriptomoedaHoje criptomoedaHoje)
        {
            if (id != criptomoedaHoje.Id)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Atualizar(criptomoedaHoje);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(_repository.Carregar(criptomoedaHoje.Id) == null))
                        return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(criptomoedaHoje);
        }

        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/deletar")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            var criptomoedaHoje = _repository.Carregar(id);

            if (criptomoedaHoje == null)
                return RedirectToAction(nameof(Error), new { message = "Criptomoeda não encontrada!" });

            return View(criptomoedaHoje);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [Route("criptomoedas-valores/deletar")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _repository.Remover(id);
            return RedirectToAction(nameof(Index));
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

        [Route("simulacao")]
        public IActionResult Simulacao(string searchString, int? busca, int? buscaVenda)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem() { Text = "Bitcoin", Value = "1", Selected = true };
            SelectListItem item2 = new SelectListItem() { Text = "Ethereum", Value = "2", Selected = false };
            SelectListItem item3 = new SelectListItem() { Text = "Bitcoin Cash", Value = "3", Selected = false };
            SelectListItem item4 = new SelectListItem() { Text = "XRP", Value = "4", Selected = false };
            SelectListItem item5 = new SelectListItem() { Text = "PAX Gold", Value = "5", Selected = false };
            SelectListItem item6 = new SelectListItem() { Text = "Litecoin", Value = "6", Selected = false };
            itens.Add(item1);
            itens.Add(item2);
            itens.Add(item3);
            itens.Add(item4);
            itens.Add(item5);
            itens.Add(item6);

            ViewBag.Busca = itens;

            List<SelectListItem> itensVenda = new List<SelectListItem>();
            SelectListItem itemVenda1 = new SelectListItem() { Text = "Bitcoin", Value = "1", Selected = true };
            SelectListItem itemVenda2 = new SelectListItem() { Text = "Ethereum", Value = "2", Selected = false };
            SelectListItem itemVenda3 = new SelectListItem() { Text = "Bitcoin Cash", Value = "3", Selected = false };
            SelectListItem itemVenda4 = new SelectListItem() { Text = "XRP", Value = "4", Selected = false };
            SelectListItem itemVenda5 = new SelectListItem() { Text = "PAX Gold", Value = "5", Selected = false };
            SelectListItem itemVenda6 = new SelectListItem() { Text = "Litecoin", Value = "6", Selected = false };
            itensVenda.Add(itemVenda1);
            itensVenda.Add(itemVenda2);
            itensVenda.Add(itemVenda3);
            itensVenda.Add(itemVenda4);
            itensVenda.Add(itemVenda5);
            itensVenda.Add(itemVenda6);
            ViewBag.buscaVenda = itensVenda;

            try
            {
                double valor = Convert.ToDouble(searchString);
                var valorHoje = 0.0;

                if (busca != null)
                    itens.Where(i => i.Value == busca.ToString()).First().Selected = true;

                if (busca == 1)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "BTC";
                        valorHoje = _repository.CarregarValorHoje("Bitcoin");

                        var total = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(total);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = total;
                    }
                }
                else if (busca == 2)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "ETH";
                        valorHoje = _repository.CarregarValorHoje("Ethereum");

                        var total = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(total);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = total;
                    }
                }
                else if (busca == 3)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "BCH";
                        valorHoje = _repository.CarregarValorHoje("Bitcoin Cash");

                        var valorTotal = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(valorTotal);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = valorTotal;
                    }
                }
                else if (busca == 4)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "XRP";
                        valorHoje = _repository.CarregarValorHoje("XRP");

                        var valorTotal = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(valorTotal);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = valorTotal;
                    }
                }
                else if (busca == 5)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "PAXG";
                        valorHoje = _repository.CarregarValorHoje("PAX Gold");

                        var valorTotal = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(valorTotal);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = valorTotal;
                    }
                }
                else if (busca == 6)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        ViewBag.Simbolo = "LTC";
                        valorHoje = _repository.CarregarValorHoje("Litecoin");

                        var valorTotal = (valor / valorHoje).ToString("F6");
                        var valorTotalDouble = Convert.ToDouble(valorTotal);

                        if (valorTotalDouble >= 1)
                            ViewBag.Total = valorTotalDouble.ToString("F2");
                        else
                            ViewBag.Total = valorTotal;
                    }
                }

                if (buscaVenda != null)
                    itensVenda.Where(i => i.Value == buscaVenda.ToString()).First().Selected = true;

                if (buscaVenda == 1)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("Bitcoin");

                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }
                else if (buscaVenda == 2)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("Ethereum");
                        
                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }
                else if (buscaVenda == 3)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("Bitcoin Cash");

                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }
                else if (buscaVenda == 4)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("XRP");

                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }
                else if (buscaVenda == 5)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("PAX Gold");

                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }
                else if (buscaVenda == 6)
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        valorHoje = _repository.CarregarValorHoje("Litecoin");

                        var valorTotal = (valor * valorHoje).ToString("F2");
                        ViewBag.TotalVenda = valorTotal;
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado ao realizar a simulação. Tente novamente." });
            }
        }
    }
}
