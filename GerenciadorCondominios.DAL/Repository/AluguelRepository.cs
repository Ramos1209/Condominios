using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GerenciadorCondominios.DAL.Repository
{
   public class AluguelRepository:RepositoryGeneric<Aluguel>, IAluguelRepository
   {
       private readonly CondominioContext _context;
        public AluguelRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public bool AluguelJaExiste(int mesId, int ano)
        {
            try
            {
                return _context.Alugueis.Any(a => a.MesId == mesId && a.Ano == ano);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public new async Task<IEnumerable<Aluguel>> PegarTodosAl()
        {
            try
            {
                return await _context.Alugueis.Include(a => a.Mes).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
