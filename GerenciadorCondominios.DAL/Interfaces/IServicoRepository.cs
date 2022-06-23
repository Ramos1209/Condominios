using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IServicoRepository:IRepositoryGeneric<Servico>
    {
        Task<IEnumerable<Servico>> PegarServicoPeloUsuarioId(string id);
    }
}
