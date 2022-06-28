using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorCondominios.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FuncoesController : Controller
    {
        private readonly IFuncaoRepository _funcaoRepository;

        public FuncoesController(IFuncaoRepository funcaoRepository)
        {
            _funcaoRepository = funcaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _funcaoRepository.GetAll());
        }

      

      [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcao funcao)
        {
            if (ModelState.IsValid)
            {
                await _funcaoRepository.AdicionarFuncao(funcao);
                TempData["NovoRegistro"] = $"Funcao {funcao.Name} adicionada";
                return RedirectToAction(nameof(Index));
            }

            return View(funcao);

        }

       [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Funcao funcao =await _funcaoRepository.GetbyId(id);
            if (funcao == null)
            {
                return NotFound();
            }
            return View(funcao);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Funcao funcao)
        {
            if (id != funcao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _funcaoRepository.AtualizarFuncao(funcao);
                TempData["Atualizacao"] = $"fucnao {funcao.Name} atualizada";
                return RedirectToAction(nameof(Index));
            }

            return View(funcao);


        }

       
        [HttpPost]
        public async Task<JsonResult> Delete(string id)
        {

            await _funcaoRepository.Excluir(id);
            TempData["Exclusao"] = $"Funcao excluida";
            return Json("Funcao excluido");

        }
    }
}
