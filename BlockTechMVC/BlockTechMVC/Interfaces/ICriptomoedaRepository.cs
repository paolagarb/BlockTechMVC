using BlockTechMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockTechMVC.Interfaces
{
    public interface ICriptomoedaRepository
    {
        Task<List<Criptomoeda>> Carregar(string searchString, string sortOrder);
        Task<Criptomoeda> Carregar(int? id);
        Task<Criptomoeda> Detalhes(int id);
        Task Adicionar(Criptomoeda criptomoeda);
        Task Editar(Criptomoeda criptomoeda);
        bool ConsultarCriptomoedaNome(string nome);
        bool ConsultarCriptomoedaSimbolo(string simbolo);
    }
}
