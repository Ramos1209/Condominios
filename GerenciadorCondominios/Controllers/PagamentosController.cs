using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;

namespace GerenciadorCondominios.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IHistoricoRecursoRepository _historicoRecursoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PagamentosController(IPagamentoRepository pagamentoRepository, IAluguelRepository aluguelRepository, IHistoricoRecursoRepository historicoRecursoRepository, IUsuarioRepository usuarioRepository)
        {
            _pagamentoRepository = pagamentoRepository;
            _aluguelRepository = aluguelRepository;
            _historicoRecursoRepository = historicoRecursoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            return View(await _pagamentoRepository.PegarPagamentoUsuario(usuario.Id));
        }

        public async Task<IActionResult> EfetuarPagamento(int id)
        {
            Pagamento pagamento = await _pagamentoRepository.GetbyId(id);
            pagamento.DataPagamento = DateTime.Now.Date;
            pagamento.Status = StatusPagamento.Pago;

            await _pagamentoRepository.Update(pagamento);

            Aluguel aluguel =  await _aluguelRepository.GetbyId(pagamento.AluguelId);

            HistoricoRecurso hr = new HistoricoRecurso
            {
                Valor = aluguel.Valor,
                MesId = aluguel.MesId,
                Ano = aluguel.Ano,
                Dia = DateTime.Now.Day,
                Tipo = Tipos.Entrada
            };

            await _historicoRecursoRepository.Insert(hr);
            TempData["NovoRegistro"] = $"Pagamento no  valor {pagamento.Aluguel.Valor} efetuado";
            return RedirectToAction(nameof(Index));
        }
    }
}
