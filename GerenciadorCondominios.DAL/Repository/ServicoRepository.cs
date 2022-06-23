using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Repository
{
    public class ServicoRepository : RepositoryGeneric<Servico>, IServicoRepository
    {
        private readonly CondominioContext _context;

        public ServicoRepository(CondominioContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servico>> PegarServicoPeloUsuarioId(string id)
        {
            return await _context.Servicos.Where(x => x.UsuarioId == id).ToListAsync();
        }
    }
}
