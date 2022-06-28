using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GerenciadorCondominios.DAL.Repository
{
    public class HistoricoRecursoRepository: RepositoryGeneric<HistoricoRecurso>,IHistoricoRecursoRepository
    {
        private readonly CondominioContext _context;
        public HistoricoRecursoRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public object PegarHistoricoGanhos(int ano)
        {
            try
            {
                return _context.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(rh => rh.Ano == ano && rh.Tipo == Tipos.Entrada).ToList()
                    .OrderBy(rh => rh.MesId)
                    .GroupBy(rh => rh.Mes.Nome)
                    .Select(rh => new { Meses = rh.Key, Valores = rh.Sum(v => v.Valor) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public object PegarHistoricoDespesas(int ano)
        {
            try
            {
                return _context.HistoricoRecursos.Include(hr => hr.Mes)
                    .Where(rh => rh.Ano ==ano && rh.Tipo == Tipos.Saida).ToList()
                    .OrderBy(rh => rh.MesId)
                    .GroupBy(rh => rh.Mes.Nome)
                    .Select(rh => new { Meses = rh.Key, Valores = rh.Sum(v => v.Valor) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<decimal> PegarSomaDespesas(int ano)
        {
            try
            {
                return await _context.HistoricoRecursos.Include(x => x.Mes)
                    .Where(x => x.Ano == ano && x.Tipo == Tipos.Saida)
                    .SumAsync(x => x.Valor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
          
        }

        public async Task<decimal> PegarSomaGanhos(int ano)
        {
            try
            {
                return await _context.HistoricoRecursos.Include(x => x.Mes)
                    .Where(x => x.Ano == ano && x.Tipo == Tipos.Entrada)
                    .SumAsync(x => x.Valor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<IEnumerable<HistoricoRecurso>> PegarUltimasMovimentacoes()
        {
            try
            {
                return await _context.HistoricoRecursos.Include(x => x.Mes).OrderByDescending(x => x.HistoricoRecursoId).Take(5)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
