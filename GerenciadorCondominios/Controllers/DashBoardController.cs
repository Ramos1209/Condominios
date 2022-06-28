using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorCondominios.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IHistoricoRecursoRepository _historicoRecursoRepository;

        public DashBoardController(IAluguelRepository aluguelRepository, IHistoricoRecursoRepository historicoRecursoRepository)
        {
            _aluguelRepository = aluguelRepository;
            _historicoRecursoRepository = historicoRecursoRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Anos"] = new SelectList(await _aluguelRepository.PegarTodosAnos());
            return View();
        }


        public JsonResult DadosGraficoGanhosAno(int ano)
        {
            return Json(_historicoRecursoRepository.PegarHistoricoGanhos(ano));
         
        }

        public JsonResult DadosGraficoDespesasAno(int ano)
        {
             return Json(_historicoRecursoRepository.PegarHistoricoDespesas(ano));
          
        }

        public async Task<JsonResult> DadosGraficoDespesasGanhosTotais()
        {
            int ano = DateTime.Now.Year;
            GanhosDespesasViewModel model = new GanhosDespesasViewModel
            {
                Despesas = await _historicoRecursoRepository.PegarSomaDespesas(ano),
                Ganhos = await _historicoRecursoRepository.PegarSomaGanhos(ano)
            };

            return Json(model);

        }
    }
}
