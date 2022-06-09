using System;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;


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
    }
}
