using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.DAL.Repository
{
    public class HistoricoRecursoRepository: RepositoryGeneric<HistoricoRecurso>,IHistoricoRecursoRepository
    {
        public HistoricoRecursoRepository(CondominioContext context) : base(context)
        {
        }
    }
}
