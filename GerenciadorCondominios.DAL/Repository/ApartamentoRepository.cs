using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCondominios.DAL.Repository
{
   public class ApartamentoRepository: RepositoryGeneric<Apartamento>, IApartamentoRepository
   {
       private readonly CondominioContext _context;
        public ApartamentoRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartamento>> PegaTodosAp()
        {
            try
            {
                return await _context.Apartamentos.Include(x => x.Morador)
                    .Include(x => x.Proprietario).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<Apartamento>> PegarTodosPeloUsuarioId(string usuarioId)
        {
            try
            {
                return await _context.Apartamentos.Include(a => a.Morador).Include(a => a.Proprietario)
                    .Where(a => a.MoradorId == usuarioId || a.ProprietarioId == usuarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
