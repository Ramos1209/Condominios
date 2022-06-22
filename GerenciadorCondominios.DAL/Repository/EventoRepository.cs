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
  public  class EventoRepository:RepositoryGeneric<Evento>, IEventoRepository
  {
      private readonly CondominioContext _context;
        public EventoRepository(CondominioContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Evento>> PegarPeloId(string usuarioId)
        {
            return await _context.Eventos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
        }
    }
}
