using BlockTechMVC.Models;
using System;
using System.Collections.Generic;

namespace BlockTechMVC.Interfaces
{
    public interface ICriptomoedaHojeRepository
    {
        List<CriptomoedaHoje> Listar();
        List<CriptomoedaHoje> ListarPorData(DateTime data);
        CriptomoedaHoje Carregar(int? id);
        List<Criptomoeda> CarregarCriptomoedas();
        void Adicionar(CriptomoedaHoje criptomoeda);
        void Atualizar(CriptomoedaHoje criptomoeda);
        void Remover(int? id);
        double CarregarValorHoje(string criptomoeda);
    }
}
