using BlockTechMVC.Data;
using BlockTechMVC.Interfaces;
using BlockTechMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlockTechMVC.Repositories
{
    public class CriptomoedaRepository : ICriptomoedaRepository
    {
        private readonly ApplicationDbContext _context;

        public CriptomoedaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Criptomoeda>> Carregar(string searchString, string sortOrder)
        {
            var criptomoedas = await (from c in _context.Criptomoeda
                                      select c).ToListAsync();

            if (!String.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.Equals("Nome_desc"))
                    criptomoedas = criptomoedas.OrderByDescending(c => c.Nome).ToList();
                else
                    criptomoedas = criptomoedas.OrderBy(s => s.Nome).ToList();
            }
            
            if (!String.IsNullOrEmpty(searchString))
                criptomoedas = criptomoedas.Where(c => c.Nome.Contains(searchString)).ToList();

            return criptomoedas;
        }
        public async Task<Criptomoeda> Carregar(int? id)
        {
            return await _context.Criptomoeda.FindAsync(id);
        }

        public async Task<Criptomoeda> Detalhes(int id)
        {
            return await _context.Criptomoeda.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Adicionar(Criptomoeda criptomoeda)
        {
            _context.Add(criptomoeda);
            await _context.SaveChangesAsync();
        }

        public bool ConsultarCriptomoedaNome(string nome)
        {
            if (_context.Criptomoeda.Any(c => c.Nome == nome))
                return true;

            return false;
        }

        public bool ConsultarCriptomoedaSimbolo(string simbolo)
        {
            if (_context.Criptomoeda.Any(c => c.Simbolo == simbolo))
                return true;

            return false;
        }

        public async Task Editar(Criptomoeda criptomoeda)
        {
            _context.Update(criptomoeda);
            await _context.SaveChangesAsync();
        }
    }
}
