using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IEventoRepository : IRepositoryGeneric<Evento>
   {
       Task<IEnumerable<Evento>> PegarPeloId(string usuarioId);
    
   }
}
