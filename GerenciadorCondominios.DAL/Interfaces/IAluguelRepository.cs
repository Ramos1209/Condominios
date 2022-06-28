using GerenciadorCondominios.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IAluguelRepository: IRepositoryGeneric<Aluguel>
   {
       bool AluguelJaExiste(int mesId, int ano);

       new Task<IEnumerable<Aluguel>> PegarTodosAl();
       Task<IEnumerable<int>> PegarTodosAnos();
   }
}
