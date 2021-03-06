using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IPagamentoRepository: IRepositoryGeneric<Pagamento>
   {
       Task<IEnumerable<Pagamento>> PegarPagamentoUsuario(string usuarioId);
   }
}
