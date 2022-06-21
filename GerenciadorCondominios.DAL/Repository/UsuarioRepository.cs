using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorCondominios.DAL.Repository
{
    public class UsuarioRepository: RepositoryGeneric<Usuario>, IUsuarioRepository
    {
        private readonly CondominioContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public UsuarioRepository(CondominioContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public int VerificaSeExistRegistro()
        {
            try
            {
                return _context.Usuarios.Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task LogarUsuario(Usuario usuario, bool lembrar)
        {
            try
            {
                await _signInManager.SignInAsync(usuario, lembrar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
        {
            try
            {
                return await _userManager.CreateAsync(usuario, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task IncluirUsuarioFuncao(Usuario usuario, string funcao)
        {
            try
            {
                await _userManager.AddToRoleAsync(usuario, funcao);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<Usuario> PegarEmailUsuario(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task DeslogarUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await _userManager.UpdateAsync(usuario);
        }

        public async Task<bool> VerificaSeUsuarioExisteEmFuncao(Usuario usuario, string funcao)
        {
            return await _userManager.IsInRoleAsync(usuario, funcao);
        }

        public  async Task<IEnumerable<string>> PegarFuncaoUsuario(Usuario usuario)
        {
            return await _userManager.GetRolesAsync(usuario);
        }

        public async Task<IdentityResult> RemoverFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes)
        {
            return await _userManager.RemoveFromRolesAsync(usuario, funcoes);
        }

        public async Task<IdentityResult> IncluirFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes)
        {
            return await _userManager.AddToRolesAsync(usuario, funcoes);
        }

        public async Task<Usuario> TakeUserByName(ClaimsPrincipal usuario)
        {
            try
            {
                return await _userManager.FindByNameAsync(usuario.Identity.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public string CodificarSenha(Usuario usuario, string senha)
        {
            try
            {
                return _userManager.PasswordHasher.HashPassword(usuario, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
