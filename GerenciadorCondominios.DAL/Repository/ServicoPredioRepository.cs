using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.DAL.Repository
{
    public class ServicoPredioRepository: RepositoryGeneric<ServicoPredio>,IServicoPredioRepository
    {
        public ServicoPredioRepository(CondominioContext context) : base(context)
        {
        }
    }
}
