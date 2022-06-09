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

    }
}
