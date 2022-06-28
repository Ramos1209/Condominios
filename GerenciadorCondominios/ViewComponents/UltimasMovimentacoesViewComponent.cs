using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCondominios.ViewComponents
{
  
    public class UltimasMovimentacoesViewComponent: ViewComponent
    {
        private readonly IHistoricoRecursoRepository _historicoRecursoRepository;

        public UltimasMovimentacoesViewComponent(IHistoricoRecursoRepository historicoRecursoRepository)
        {
            _historicoRecursoRepository = historicoRecursoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _historicoRecursoRepository.PegarUltimasMovimentacoes());
        }
    }
}
