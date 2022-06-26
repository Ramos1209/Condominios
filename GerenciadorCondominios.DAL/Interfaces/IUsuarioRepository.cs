using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorCondominios.DAL.Interfaces
{
    public interface IUsuarioRepository: IRepositoryGeneric<Usuario>
    {
        int VerificaSeExistRegistro();
        Task LogarUsuario(Usuario usuario, bool lembrar);
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
        Task IncluirUsuarioFuncao(Usuario usuario, string funcao);
        Task<Usuario> PegarEmailUsuario(string email);
        Task DeslogarUser();
        Task AtualizarUsuario(Usuario usuario);
        Task<bool> VerificaSeUsuarioExisteEmFuncao(Usuario usuario, string funcao);
        Task<IList<string>> PegarFuncaoUsuario(Usuario usuario);
        Task<IdentityResult> RemoverFuncaoUsuario(Usuario usuario, IEnumerable<string> funcoes);
        Task<IdentityResult> IncluirFuncaoUsuario(Usuario usuario, IEnumerable<string>funcoes);
        Task<Usuario> PegarUSuarioPeloNome(ClaimsPrincipal usuario);

        string CodificarSenha(Usuario usuario, string senha);
    }
}
