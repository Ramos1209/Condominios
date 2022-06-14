using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.DAL.Repository
{
    public class FuncaoRepository:RepositoryGeneric<Funcao>, IFuncaoRepository
    {
        public FuncaoRepository(CondominioContext context) : base(context)
        {
        }
    }
}
