using BlockTechMVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockTechMVC.Interfaces
{
    public interface ICriptomoedaHojeRepository
    {
        Task<List<CriptomoedaHoje>> Listar();
        Task<List<CriptomoedaHoje>> ListarPorData(DateTime data);
        CriptomoedaHoje Carregar(int? id);
        List<Criptomoeda> CarregarCriptomoedas();
        Task Adicionar(CriptomoedaHoje criptomoeda);
        void Atualizar(CriptomoedaHoje criptomoeda);
        void Remover(int? id);
        double CarregarValorHoje(string criptomoeda);
    }
}
