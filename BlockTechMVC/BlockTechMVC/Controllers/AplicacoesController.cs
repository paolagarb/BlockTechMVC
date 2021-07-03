using BlockTechMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlockTechMVC.Controllers
{
    public class AplicacoesController : Controller
    {
        private readonly IAplicacaoRepository _repository;

        public AplicacoesController(IAplicacaoRepository repository)
        {
            _repository = repository;
        }

        [Route("aplicacoes")]
        public async Task<IActionResult> Index(int? id, string searchString, int? busca, string sortOrder)
        {
            var user = User.Identity.Name;

            ViewBag.Quantidade = sortOrder == "Quantidade" ? "Quantidade_desc" : "Quantidade";
            ViewBag.Criptomoeda = sortOrder == "Criptomoeda" ? "Criptomoeda_desc" : "Criptomoeda";

            if (user == "Administrador")
            {
                ViewBag.Nome = sortOrder == "Nome" ? "Nome_desc" : "Nome";

                List<SelectListItem> itens = new List<SelectListItem>();
                SelectListItem item1 = new SelectListItem() { Text = "Nome/Razão Social", Value = "1", Selected = true };
                SelectListItem item2 = new SelectListItem() { Text = "Criptomoeda", Value = "2", Selected = false };
                itens.Add(item1);
                itens.Add(item2);

                ViewBag.Busca = itens;

                var saldoCriptomoeda = _repository.CarregarSaldo();

                if (sortOrder != null)
                {
                    var orderName = saldoCriptomoeda.OrderBy(t => t.ContaCliente.ApplicationUser.Nome);

                    switch (sortOrder)
                    {
                        case "Nome_desc":
                            orderName = orderName.OrderByDescending(s => s.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Nome":
                            orderName = saldoCriptomoeda.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
                            break;
                        case "Quantidade":
                            orderName = saldoCriptomoeda.OrderBy(s => s.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = saldoCriptomoeda.OrderByDescending(s => s.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = saldoCriptomoeda.OrderBy(s => s.Criptomoeda);
                            break;
                        case "Criptomoeda_desc":
                            orderName = saldoCriptomoeda.OrderByDescending(s => s.Criptomoeda);
                            break;
                        default:
                            orderName = saldoCriptomoeda.OrderBy(s => s.ContaCliente.ApplicationUser.Nome);
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
                        var usuarioSelecionado = _repository.CarregarSaldoPorUserContain(searchString);

                        return View(usuarioSelecionado);
                    }
                    else if (busca == 2)
                    {
                        var usuarioSelecionado = _repository.CarregarSaldoPorCriptoContain(searchString);

                        return View(usuarioSelecionado);
                    }
                }

                return View(await saldoCriptomoeda.ToListAsync());
            }
            else
            {
                var usuario = _repository.CarregarSaldoPorUser(user);

                if (String.IsNullOrEmpty(sortOrder))
                {
                    var orderName = usuario.OrderBy(t => t.Criptomoeda);

                    switch (sortOrder)
                    {
                        case "Quantidade":
                            orderName = usuario.OrderBy(s => s.Quantidade);
                            break;
                        case "Quantidade_desc":
                            orderName = usuario.OrderByDescending(s => s.Quantidade);
                            break;
                        case "Criptomoeda":
                            orderName = usuario.OrderBy(s => s.Criptomoeda);
                            break;
                        case "Criptomoeda_desc":
                            orderName = usuario.OrderByDescending(s => s.Criptomoeda);
                            break;
                        default:
                            orderName = usuario.OrderBy(s => s.Criptomoeda);
                            break;
                    };

                    return View(orderName.ToList());
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    var criptoSelecionada = _repository.CarregarSaldoPorCripto(user, searchString);

                    return View(criptoSelecionada.ToList());
                }

                return View(usuario);
            }
        }
    }
}
