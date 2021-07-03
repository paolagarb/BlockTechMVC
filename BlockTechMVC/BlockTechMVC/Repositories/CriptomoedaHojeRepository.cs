using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockTechMVC.Repositories
{
    public class CriptomoedaHojeRepository : ICriptomoedaHojeRepository
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedaHojeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(CriptomoedaHoje criptomoeda)
        {
            _context.Add(criptomoeda);
            _context.SaveChangesAsync();
        }

        public void Atualizar(CriptomoedaHoje criptomoeda)
        {
            _context.Update(criptomoeda);
            _context.SaveChangesAsync();
        }

        public CriptomoedaHoje Carregar(int? id)
        {
            return _context.CriptomoedaHoje
                    .Include(c => c.Criptomoeda)
                    .FirstOrDefault(m => m.Id == id);
        }

        public List<Criptomoeda> CarregarCriptomoedas()
        {
            return _context.Criptomoeda.ToList();
        }

        public List<CriptomoedaHoje> Listar()
        {
            return _context.CriptomoedaHoje
                    .Where(c => c.Data.Equals(DateTime.Now.Date))
                    .Include(c => c.Criptomoeda).ToList();
        }

        public List<CriptomoedaHoje> ListarPorData(DateTime data)
        {
            return _context.CriptomoedaHoje
                     .Where(c => c.Data.Equals(data))
                     .Include(c => c.Criptomoeda).ToList();
        }

        public void Remover(int? id)
        {
            var criptomoeda = Carregar(id);
            _context.CriptomoedaHoje.Remove(criptomoeda);
            _context.SaveChangesAsync();
        }

        public double CarregarValorHoje(string criptomoeda)
        {
            return (from c in _context.CriptomoedaHoje
                    join cripto in _context.Criptomoeda
                    on c.CriptomoedaId equals cripto.Id
                    where cripto.Nome == criptomoeda &&
                    c.Data == DateTime.Today
                    select c.Valor).Single();
        }
    }
}
