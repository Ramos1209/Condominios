using System;
using System.Threading.Tasks;
using GerenciadorCondominios.BLL.Models;
using GerenciadorCondominios.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorCondominios.DAL.Repository
{
    public class FuncaoRepository:RepositoryGeneric<Funcao>, IFuncaoRepository
    {
        private readonly RoleManager<Funcao> _gerenciarFuncao;
        public FuncaoRepository(CondominioContext context, RoleManager<Funcao> gerenciarFuncao) : base(context)
        {
            _gerenciarFuncao = gerenciarFuncao;
        }

        public async Task AdicionarFuncao(Funcao funcao)
        {
            try
            {
                funcao.Id = Guid.NewGuid().ToString();
                await _gerenciarFuncao.CreateAsync(funcao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public new async Task AtualizarFuncao(Funcao funcao)
        {
            try
            {
                Funcao f = await GetbyId(funcao.Id);
                f.Name = funcao.Name;
                f.NormalizedName = funcao.NormalizedName;
                f.Descricao = funcao.Descricao;
                await _gerenciarFuncao.UpdateAsync(f);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
          
        }
    }
}
