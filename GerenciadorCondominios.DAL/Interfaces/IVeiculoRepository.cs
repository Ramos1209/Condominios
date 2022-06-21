using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IVeiculoRepository: IRepositoryGeneric<Veiculo>
    {
        Task<IEnumerable<Veiculo>> TakeVeiculoPorUsuario(string usuarioId);
    }
}
