using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GerenciadorCondominios.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorCondominios.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHistoricoRecursoRepository _historicoRecursoRepository;
        private readonly IServicoPredioRepository _predioRepository;

        public ServicoController(IServicoRepository servicoRepository, IUsuarioRepository usuarioRepository, IHistoricoRecursoRepository historicoRecursoRepository, IServicoPredioRepository predioRepository)
        {
            _servicoRepository = servicoRepository;
            _usuarioRepository = usuarioRepository;
            _historicoRecursoRepository = historicoRecursoRepository;
            _predioRepository = predioRepository;
        }


        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            if (await _usuarioRepository.VerificaSeUsuarioExisteEmFuncao(usuario, "Morador"))
            {
                return View(await _servicoRepository.PegarServicoPeloUsuarioId(usuario.Id));
            }
            return View(await _servicoRepository.GetAll());
        }

       [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            Servico servico = new Servico
            {
                UsuarioId = usuario.Id
            };
            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Status = StatuServico.Pendente;
                await _servicoRepository.Insert(servico);
                TempData["NovoRegistro"] = $"Servico {servico.Nome} inserido";
                return RedirectToAction(nameof(Index));

            }
            return View(servico);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Servico servico = await _servicoRepository.GetbyId(id);
            if (servico == null)
            {
                return NotFound();
            }
            return View(servico);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {

            if (id != servico.ServicoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _servicoRepository.Update(servico);
                TempData["Atualizado"] = $"Servico {servico.Nome} atualizado";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _servicoRepository.Excluir(id);
            TempData["Exclusao"] = $"Servico  excluido";
            return Json("Servico excluido");
        }

        [Authorize(Roles = "Administrador, Sindico")]
        [HttpGet]
        public async Task<IActionResult> AprovarServico(int id)
        {
            Servico servico = await _servicoRepository.GetbyId(id);
            ServicoAprovadoViewModel model = new ServicoAprovadoViewModel
            {
                ServicoId = servico.ServicoId,
                Nome = servico.Nome
            };
            return View(model);
        }
        [Authorize(Roles = "Administrador, Sindico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprovarServico(ServicoAprovadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Servico servico = await _servicoRepository.GetbyId(viewModel.ServicoId);
                servico.Status = StatuServico.Aceito;
                await _servicoRepository.Update(servico);

                ServicoPredio predio = new ServicoPredio
                {
                    ServicoId = viewModel.ServicoId,
                    DataExecusao = viewModel.Data
                };

                await _predioRepository.Insert(predio);

                HistoricoRecurso hr = new HistoricoRecurso
                {
                    Valor = servico.Valor,
                    MesId = predio.DataExecusao.Month,
                    Dia = predio.DataExecusao.Day,
                    Ano = predio.DataExecusao.Year,
                    Tipo = Tipos.Saida
                };
                await _historicoRecursoRepository.Insert(hr);
                TempData["NovoRegistro"] = $"Serviço {servico.Nome} escalado com sucesso";

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        [Authorize(Roles = "Administrador, Sindico")]
        public async Task<IActionResult> RecusarServico(int id)
        {
            Servico servico = await _servicoRepository.GetbyId(id);
            if (servico == null)
                return NotFound();

            servico.Status = StatuServico.Resusado;
            await _servicoRepository.Update(servico);

            TempData["Exclusao"] = $"{servico.Nome} recusado";
            return RedirectToAction(nameof(Index));
        }
    }
}
