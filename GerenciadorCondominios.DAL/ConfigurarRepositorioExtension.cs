using GerenciadorCondominios.DAL.Interfaces;
using GerenciadorCondominios.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorCondominios.DAL
{
    public static class ConfigurarRepositorioExtension
    {
        public static void ConfigurarRepositorio(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IFuncaoRepository, FuncaoRepository>();
        }
    }
}
