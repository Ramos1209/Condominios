using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorCondominios.Controllers
{
    public class ApartamentosController : Controller
    {

        private readonly IApartamentoRepository _apartamentoRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUsuarioRepository _usuarioRepository;

        public ApartamentosController(IApartamentoRepository apartamentoRepository, IWebHostEnvironment webHostEnvironment, IUsuarioRepository usuarioRepository)
        {
            _apartamentoRepository = apartamentoRepository;
            _webHostEnvironment = webHostEnvironment;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.PegarUSuarioPeloNome(User);
            if (await _usuarioRepository.VerificaSeUsuarioExisteEmFuncao(usuario, "Sindico"))
            {
                return View(await _apartamentoRepository.PegaTodosAp());
            }
            return View(await _apartamentoRepository.PegarTodosPeloUsuarioId(usuario.Id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["MoradorId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName");
            ViewData["ProprietarioId"]= new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName");
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Apartamento apartamento, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nameFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nameFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        apartamento.Foto = "~/Imagens/" + nameFoto;

                    }
                }
                await _apartamentoRepository.Insert(apartamento);
                TempData["NovoRegistro"] = $"Apartamento numero {apartamento.Numero} registrado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoradorId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName",apartamento.MoradorId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName",apartamento.ApartamentoId);
            return View(apartamento);

        }

       [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Apartamento apartamento = await _apartamentoRepository.GetbyId(id);
            if (apartamento == null)
                return NotFound();
            TempData["foto"] = apartamento.Foto;
            ViewData["MoradorId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName", apartamento.MoradorId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName", apartamento.ApartamentoId);
            return View(apartamento);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Apartamento apartamento, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nameFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nameFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        apartamento.Foto = "~/Imagens/" + nameFoto;
                        System.IO.File.Delete(TempData["foto"].ToString().Replace("~", "wwwroot"));

                    }
                }
                else
                {
                    apartamento.Foto = TempData["foto"].ToString();
                }

                await _apartamentoRepository.Update(apartamento);
                TempData["Atualizacao"] = $"Apartamento numero {apartamento.Numero} atualizado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            ViewData["MoradorId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName", apartamento.MoradorId);
            ViewData["ProprietarioId"] = new SelectList(await _usuarioRepository.GetAll(), "Id", "UserName", apartamento.ApartamentoId);
            return View(apartamento);
        }

    

        // POST: ApartamentosController/Delete/5
        [HttpPost]
     
        public async Task<JsonResult> Delete(int id)
        {

            Apartamento ap = await _apartamentoRepository.GetbyId(id);
            System.IO.File.Delete(ap.Foto.Replace("~", "wwwroot"));
            await _apartamentoRepository.Excluir(ap);
            TempData["Exclusao"] = $"Apartamento numero {ap.Numero} excluido";
            return Json("Apartemento excluido com sucesso");


        }
    }
}
