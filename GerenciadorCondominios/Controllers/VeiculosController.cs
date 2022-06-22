using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public VeiculosController( IVeiculoRepository veiculoRepository, IUsuarioRepository usuarioRepository)
        {
          
            _veiculoRepository = veiculoRepository;
            _usuarioRepository = usuarioRepository;
        }

      

       [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            try
            {
                Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
                veiculo.UsuarioId = usuario.Id;
                await _veiculoRepository.Insert(veiculo);
                return RedirectToAction("MinhasInformacao", "Usuarios");
            }
            catch
            {
                return View(veiculo);
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var veiculo = await _veiculoRepository.GetbyId(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {

            if (id != veiculo.VeiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                  await _veiculoRepository.Update(veiculo);
                  return RedirectToAction("MinhasInformacao", "Usuarios");
                
                
            }
            return View(veiculo);

        }

     
        [HttpPost]
        public async Task<JsonResult>  Delete(int id)
        {
             await _veiculoRepository.Excluir(id);
             return Json("Veiculo excluido com sucesso!!");

        }
    }
}
