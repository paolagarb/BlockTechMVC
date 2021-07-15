using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTechMVC.Repositories
{
    public class CriptomoedaHojeRepository : ICriptomoedaHojeRepository
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedaHojeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(CriptomoedaHoje criptomoeda)
        {
            _context.Add(criptomoeda);
            await _context.SaveChangesAsync();
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

        public async Task<List<CriptomoedaHoje>> Listar()
        {
            //Caso mais de um usuário entre na página,
            //o valor das criptomoedas serão salvos ao mesmo tempo,
            //tornando essa filtragem ineficiente - seriam exibidas criptomoedas repetidas
            //int criptomoedas = _context.Criptomoeda.Count();
            //return await _context.CriptomoedaHoje
            //        .OrderByDescending(c => c.Data)
            //        .Where(c => c.Data.Date.Equals(DateTime.Now.Date))
            //        .Take(criptomoedas)
            //        .Include(c => c.Criptomoeda).ToListAsync();

            var criptomoedas = _context.Criptomoeda.ToList();
            List<CriptomoedaHoje> list = new List<CriptomoedaHoje>();

            foreach (var item in criptomoedas)
            {
                var criptomoeda = await _context.CriptomoedaHoje
                            .OrderByDescending(c => c.Data)
                            .Where(c => c.Data.Date.Equals(DateTime.Now.Date) && c.Criptomoeda.Nome.Equals(item.Nome))
                            .LastOrDefaultAsync();

                list.Add(criptomoeda);
            }

            return list;
        }

        public async Task<List<CriptomoedaHoje>> ListarPorData(DateTime data)
        {
            int criptomoedas = _context.Criptomoeda.Count();

            return await _context.CriptomoedaHoje
                     .OrderByDescending(c => c.Data)
                     .Where(c => c.Data.Equals(data))
                     .Take(criptomoedas)
                     .Include(c => c.Criptomoeda).ToListAsync();
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
