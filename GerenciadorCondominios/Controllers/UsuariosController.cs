﻿using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFuncaoRepository _funcaoRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsuariosController(IUsuarioRepository usuarioRepository, IWebHostEnvironment webHostEnvironment, IFuncaoRepository funcaoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _webHostEnvironment = webHostEnvironment;
            _funcaoRepository = funcaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _usuarioRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "Imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

                Usuario usuario = new Usuario();
                IdentityResult usuarioCriado;

                if (_usuarioRepository.VerificaSeExistRegistro() == 0)
                {
                    usuario.UserName = model.Nome;
                    usuario.Cpf = model.Cpf;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PromeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovado;

                    usuarioCriado = await _usuarioRepository.CriarUsuario(usuario, model.Senha);
                    if (usuarioCriado.Succeeded)
                    {
                        await _usuarioRepository.IncluirUsuarioFuncao(usuario, "Administrador");
                        await _usuarioRepository.LogarUsuario(usuario, false);
                        return RedirectToAction("Index", "Usuarios");
                    }

                }

                usuario.UserName = model.Nome;
                usuario.Cpf = model.Cpf;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PromeiroAcesso = false;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _usuarioRepository.CriarUsuario(usuario, model.Senha);
                if (usuarioCriado.Succeeded)
                {
                    return View("Analise", usuario.UserName);
                }
                else
                {
                    foreach (IdentityError error in usuarioCriado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepository.DeslogarUser();
            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepository.PegarEmailUsuario(model.Email);

                if (usuario != null)
                {
                    if (usuario.Status == StatusConta.Analisando)
                    {
                        return View("Analise", usuario.UserName);
                    }
                    else if (usuario.Status == StatusConta.Reprovado)
                    {
                        return View("Reprovado", usuario.UserName);
                    }

                    else if (usuario.PromeiroAcesso == true)
                    {
                        return View("RedefinirSenha", usuario);
                    }

                    else
                    {
                        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
                        if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) !=
                            PasswordVerificationResult.Failed)
                        {
                            await _usuarioRepository.LogarUsuario(usuario, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuarios e/ou senha Invalidos");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuarios e/ou senha Invalidos");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _usuarioRepository.DeslogarUser();
            return RedirectToAction("Login");
        }
        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }

        public async Task<JsonResult> AprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepository.GetbyId(usuarioId);
            usuario.Status = StatusConta.Aprovado;
            await _usuarioRepository.IncluirUsuarioFuncao(usuario, "Morador");
            await _usuarioRepository.AtualizarUsuario(usuario);
            return Json(true);
        }

        public async Task<JsonResult> ReprovadoUsuario(string usuarioId)
        {
            Usuario usuario = await _usuarioRepository.GetbyId(usuarioId);
            usuario.Status = StatusConta.Reprovado;
            await _usuarioRepository.AtualizarUsuario(usuario);
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GerenciarUsuario(string usuarioId, string nome)
        {
            if (usuarioId == null)
                return NotFound();

            TempData["usuarioId"] = usuarioId;
            ViewBag.Nome = nome;

            Usuario usuario = await _usuarioRepository.GetbyId(usuarioId);
            if (usuario == null)
                return NotFound();

            List<FuncaoUsuarioViewModel> viewModels = new List<FuncaoUsuarioViewModel>();

            foreach (Funcao funcao in await _funcaoRepository.GetAll())
            {
                FuncaoUsuarioViewModel model = new FuncaoUsuarioViewModel
                {
                    funcaoId = funcao.Id,
                    Nome = funcao.Name,
                    descricao = funcao.Descricao
                };

                if (await _usuarioRepository.VerificaSeUsuarioExisteEmFuncao(usuario, funcao.Name))
                {
                    model.isSelecioanda = true;
                }
                else
                
                    model.isSelecioanda = false;


                viewModels.Add(model);
                

            }

            return View(viewModels);
        }

        public async Task<IActionResult> GerenciarUsuario(IEnumerable<FuncaoUsuarioViewModel> models)
        {
            string usuarioId = TempData["usuarioId"].ToString();

            Usuario usuario = await _usuarioRepository.GetbyId(usuarioId);
            if (usuario == null)
                return NotFound();

            IEnumerable<string> funcoes = await _usuarioRepository.PegarFuncaoUsuario(usuario);
            IdentityResult resultado = await _usuarioRepository.RemoverFuncaoUsuario(usuario, funcoes);

            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("","Não foi possivel atualizar as funções do usuario");
                return View("GerenciarUsuario", usuarioId);
            }

            resultado = await _usuarioRepository.IncluirFuncaoUsuario(usuario,
                models.Where(x => x.isSelecioanda = true).Select(x => x.Nome));
            if (!resultado.Succeeded)
            {
                ModelState.AddModelError("", "Não foi possivel atualizar as funções do usuario");
                return View("GerenciarUsuario", usuarioId);
            }


            return RedirectToAction(nameof(Index));
        }

    }
}
