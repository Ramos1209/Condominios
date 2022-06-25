using System.Collections;
using System.Collections.Generic;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorCondominios.Controllers
{
    public class AlugueisController : Controller
    {
        private readonly IAluguelRepository _aluguelRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMesRepository _mesRepository;

        public AlugueisController(IAluguelRepository aluguelRepository, IUsuarioRepository usuarioRepository, IPagamentoRepository pagamentoRepository, IMesRepository mesRepository)
        {
            _aluguelRepository = aluguelRepository;
            _usuarioRepository = usuarioRepository;
            _pagamentoRepository = pagamentoRepository;
            _mesRepository = mesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _aluguelRepository.PegarTodosAl());
        }

     
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MesId"] = new SelectList(await _mesRepository.PegarTodosMes(), "MesId", "Nome");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                if (!_aluguelRepository.AluguelJaExiste(aluguel.MesId, aluguel.Ano))
                {
                    await _aluguelRepository.Insert(aluguel);
                    IEnumerable<Usuario> usuarios = await _usuarioRepository.GetAll();
                    Pagamento pagamento;
                    List<Pagamento> pagamentos = new List<Pagamento>();

                    foreach (Usuario u in usuarios)
                    {
                        pagamento = new Pagamento
                        {
                            AluguelId = aluguel.AluguelId,
                            UsuarioId = u.Id,
                            DataPagamento = null,
                            Status = StatusPagamento.Pendente
                        };

                        pagamentos.Add(pagamento);
                    }

                    await _pagamentoRepository.Insert(pagamentos);
                    TempData["NovoRegistro"] = $"Pagamento adicionado com sucesso";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError("","Alugeul ja existe");
                    ViewData["MesId"] = new SelectList(await _mesRepository.PegarTodosMes(), "MesId", "Nome");
                    return View(aluguel);
                }
            }

            ViewData["MesId"] = new SelectList(await _mesRepository.PegarTodosMes(), "MesId", "Nome");
            return View(aluguel);

        }

       [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Aluguel aluguel = await _aluguelRepository.GetbyId(id);
            if (aluguel == null)
            {
                NotFound();
            }

            ViewData["MesId"] = new SelectList(await _mesRepository.PegarTodosMes(), "MesId", "Nome");
            return View(aluguel);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluguel aluguel)
        {
            if (id != aluguel.AluguelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _aluguelRepository.Update(aluguel);
                TempData["Atualizacao"] = $"Aluguel {aluguel.Valor} atualizado";
                return RedirectToAction(nameof(Index));
            }

            return View(aluguel);

        }

      
      
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _aluguelRepository.Excluir(id);
            TempData["Exclusao"] = $"Aluguel excluido";
            return Json("Aluguel excluido");
        }
    }
}
