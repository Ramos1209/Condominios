using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IFuncaoRepository : IRepositoryGeneric<Funcao>
    {
        Task AdicionarFuncao(Funcao funcao);

        new Task AtualizarFuncao(Funcao funcao);
    }
}
