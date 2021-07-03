using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Controllers
{
    [Authorize]
    public class TransacoesController : Controller
    {
        private readonly ITransacaoRepository _repository;

        public TransacoesController(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        [Route("transacoes/index")]
        public async Task<IActionResult> Index(int? busca, string searchString, string sortOrder)
        {
            var user = User.Identity.Name;
            try
            {
                ViewBag.Data = sortOrder == "Data" ? "Data_desc" : "Data";
                ViewBag.Quantidade = sortOrder == "Quantidade" ? "Quantidade_desc" : "Quantidade";
                ViewBag.Criptomoeda = sortOrder == "Criptomoeda" ? "Criptomoeda_desc" : "Criptomoeda";
                ViewBag.Valor = sortOrder == "Valor" ? "Valor_desc" : "Valor";

                if (user == "Administrador")
                {
                    ViewBag.Nome = sortOrder == "Nome" ? "Nome_desc" : "Nome";

                    List<SelectListItem> itens = new List<SelectListItem>();
                    SelectListItem item1 = new SelectListItem() { Text = "Cliente", Value = "1", Selected = true };
                    SelectListItem item2 = new SelectListItem() { Text = "Criptomoeda", Value = "2", Selected = false };
                    itens.Add(item1);
                    itens.Add(item2);

                    ViewBag.Busca = itens;

                    var transacao = _repository.Carregar();

                    if (String.IsNullOrEmpty(sortOrder))
                    {
                        var orderName = transacao.OrderBy(t => t.ContaCliente.ApplicationUser.Nome);

                        switch (sortOrder)
                        {
                            case "Nome_desc":
                                orderName = orderName.OrderByDescending(s => s.ContaCliente.ApplicationUser.Nome);
                                break;
                            case "Nome":
                                orderName = transacao.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
                                break;
                            case "Data":
                                orderName = transacao.OrderBy(s => s.Data);
                                break;
                            case "Data_desc":
                                orderName = transacao.OrderByDescending(s => s.Data);
                                break;
                            case "Criptomoeda":
                                orderName = transacao.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Criptomoeda_desc":
                                orderName = transacao.OrderByDescending(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Valor":
                                orderName = transacao.OrderBy(s => s.Valor);
                                break;
                            case "Valor_desc":
                                orderName = transacao.OrderByDescending(s => s.Valor);
                                break;
                            default:
                                orderName = transacao.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
                                break;
                        };

                        return View(orderName.ToList());
                    }

                    if (busca != null)
                        itens.Where(i => i.Value == busca.ToString()).First().Selected = true;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        if (busca == 1)
                        {
                            var usuarioSelecionado = _repository.Carregar(searchString);

                            return View(usuarioSelecionado);
                        }
                        else if (busca == 2)
                        {
                            var usuarioSelecionado = _repository.CarregarPorCriptomoedaAdm(searchString);

                            return View(usuarioSelecionado);
                        }
                    }

                    return View(transacao);
                }
                else
                {
                    var usuario = _repository.CarregarPorUsuario(user);

                    if (String.IsNullOrEmpty(sortOrder))
                    {
                        var orderName = usuario.OrderBy(t => t.CriptomoedaHoje.Criptomoeda.Nome);

                        switch (sortOrder)
                        {
                            case "Data":
                                orderName = usuario.OrderBy(s => s.Data);
                                break;
                            case "Data_desc":
                                orderName = usuario.OrderByDescending(s => s.Data);
                                break;
                            case "Quantidade":
                                orderName = usuario.OrderBy(s => s.CriptoSaldo.Quantidade);
                                break;
                            case "Quantidade_desc":
                                orderName = usuario.OrderByDescending(s => s.CriptoSaldo.Quantidade);
                                break;
                            case "Criptomoeda":
                                orderName = usuario.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Criptomoeda_desc":
                                orderName = usuario.OrderByDescending(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                            case "Valor":
                                orderName = usuario.OrderBy(s => s.Valor);
                                break;
                            case "Valor_desc":
                                orderName = usuario.OrderByDescending(s => s.Valor);
                                break;
                            default:
                                orderName = usuario.OrderBy(s => s.CriptomoedaHoje.Criptomoeda.Nome);
                                break;
                        };

                        return View(orderName);
                    }

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        var criptoSelecionada = _repository.CarregarPorCriptomoeda(searchString, user);

                        return View(criptoSelecionada.ToList());
                    }

                    return View(usuario);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Ocorreu um erro inesperado! Tente novamente em alguns instantes." });
            }
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
