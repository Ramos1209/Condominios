using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IApartamentoRepository: IRepositoryGeneric<Apartamento>
    {
        new Task<IEnumerable<Apartamento>> PegaTodosAp();
        Task<IEnumerable<Apartamento>> PegarTodosPeloUsuarioId(string usuarioId);
    }
}
