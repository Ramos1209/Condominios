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
   public class PagamentoRepository: RepositoryGeneric<Pagamento>, IPagamentoRepository
   {

       private readonly CondominioContext _context;
        public PagamentoRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagamento>> PegarPagamentoUsuario(string usuarioId)
        {
            try
            {
                return await _context.Pagamentos.Include(a=>a.Aluguel)
                                                .ThenInclude(p=>p.Mes)
                                                .Where(x => x.UsuarioId == usuarioId).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
