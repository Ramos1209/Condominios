using System;
using System.Collections.Generic;
using System.Text;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.DAL.Repository
{
   public class PagamentoRepository: RepositoryGeneric<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(CondominioContext context) : base(context)
        {
        }
    }
}
