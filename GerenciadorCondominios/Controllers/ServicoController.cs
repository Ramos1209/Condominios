using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ServicoController(IServicoRepository servicoRepository, IUsuarioRepository usuarioRepository)
        {
            _servicoRepository = servicoRepository;
            _usuarioRepository = usuarioRepository;
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


        // GET: ServicoController/Create
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

        // POST: ServicoController/Edit/5
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
    }
}
