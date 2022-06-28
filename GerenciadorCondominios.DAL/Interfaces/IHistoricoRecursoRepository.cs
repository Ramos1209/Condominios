using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IHistoricoRecursoRepository : IRepositoryGeneric<HistoricoRecurso>
    {
        object PegarHistoricoGanhos(int ano);
        object PegarHistoricoDespesas(int ano);

        Task<decimal> PegarSomaDespesas(int ano);
        Task<decimal> PegarSomaGanhos(int ano);

        Task<IEnumerable<HistoricoRecurso>> PegarUltimasMovimentacoes();
    }
}
